using HotaRmgTemplateEditor.Dialogs;
using HotaRmgTemplateEditor.Domain.RmgFormat;
using HotaRmgTemplateEditor.ViewModels;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


namespace HotaRmgTemplateEditor.UserControls
{
	/// <summary>
	/// Interaction logic for Template.xaml
	/// </summary>
	public partial class Template : UserControl
	{
		public TemplateViewModel ActiveTemplate
		{
			get { return (TemplateViewModel)GetValue(ActiveTemplateProperty); }
			set { SetValue(TemplateProperty, value); }
		}
		public static readonly DependencyProperty ActiveTemplateProperty =
			DependencyProperty.Register("ActiveTemplate", typeof(TemplateViewModel), typeof(Template), new PropertyMetadata(null, ActiveTemplateChanged));

		public double CurrentScale
		{
			get { return (double)GetValue(CurrentScaleProperty); }
			set { SetValue(CurrentScaleProperty, value); }
		}
		public static readonly DependencyProperty CurrentScaleProperty =
			DependencyProperty.Register("CurrentScale", typeof(double), typeof(Template), new PropertyMetadata(1.0d, CurrentScaleChanged, CurrentScaleCoerceCallback));

		private static void ActiveTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = (Template)d;
			var vm = (TemplateViewModel)e.NewValue;
			control.Redraw(vm?.GetBaseTemplate());
		}

		private static object CurrentScaleCoerceCallback(DependencyObject d, object baseValue)
		{
			var scale = (double)baseValue;

			if (scale == double.MinValue)
			{
				return d.GetValue(CurrentScaleProperty);
			}

			if (scale > 3.0)
			{
				return 3.0;
			}
			else if (scale < 0.2)
			{
				return 0.2;
			}
			else
			{
				return scale;
			}
		}

		private static void CurrentScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = (Template)d;
			control.Rescale();
		}

		private Point InitialMousePosition;
		private Vector CurrentOffsetVector;
		private Vector TempVector;

		private bool IsDraggingZone;
		private Zone? SelectedElement;
		private Vector DraggingDelta;

		private double? MirrorLeft;
		private bool IsMirrorTemplate;
		private Dictionary<int, Zone> ZoneUcDict;

		private Dictionary<Zone, List<UIElement>> ConnectedUIElementsDict { get; }
		private Dictionary<UIElement, (Zone, Zone)> UIElementEndpointsDict { get; }
		private Dictionary<UIElement, UIElementMetadata> UIElementMetadataDict { get; }
		private HashSet<UIElement> HighlightedElements { get; }

		private class UIElementMetadata
		{
			public int Index { get; set; }
			public int LinkCount { get; set; }

			public UIElementMetadata(int index, int linkCount)
			{
				Index = index;
				LinkCount = linkCount;
			}
		}

		public Template()
		{
			InitializeComponent();

			ConnectedUIElementsDict = [];
			UIElementEndpointsDict = [];
			UIElementMetadataDict = [];

			ZoneUcDict = [];
			HighlightedElements = [];
		}

		public void ZoomIn()
		{
			CurrentScale *= Constants.ZoomInMultiplier;
		}

		public void ZoomOut()
		{
			CurrentScale *= Constants.ZoomOutMultiplier;
		}

		public void ZoomToFit()
		{
			CenterTemplate();
		}

		private void ClearHighlights()
		{
			foreach (var prevHighlight in HighlightedElements)
			{
				if (prevHighlight is Line tmpLine)
				{
					var style = (Style)FindResource(tmpLine.Tag);
					foreach (var setter in style.Setters)
					{
						if (setter is Setter tmpSetter && tmpSetter.Property.Name == nameof(Line.Stroke))
						{
							tmpLine.Stroke = (Brush)tmpSetter.Value;
							break;
						}
					}
				}
				else if (prevHighlight is Zone tmpZone)
				{
					tmpZone.BorderColor = Brushes.Black;
				}
			}

			HighlightedElements.Clear();
		}

		private Line? LineClicked(object sender, MouseButtonEventArgs e)
		{
			var mousePosition = LayoutTransform.Inverse.Transform(e.GetPosition(this));
			var adjustedMousePos = new Point(mousePosition.X / CurrentScale, mousePosition.Y / CurrentScale);
			var mouseRect = new Rect(adjustedMousePos.X - Constants.LineDistance / 2, adjustedMousePos.Y - Constants.LineDistance / 2, Constants.LineDistance, Constants.LineDistance);

			foreach (var child in canvas.Children)
			{
				if (child is not Line line)
				{
					continue;
				}

				if (Intersects(mouseRect, line))
				{
					if (UIElementEndpointsDict.TryGetValue(line, out var endpoints) &&
						endpoints.Item1.IsMirrorZone && endpoints.Item2.IsMirrorZone)
					{
						continue;
					}

					return line;
				}
			}

			return null;
		}

		private bool Intersects(Rect rect, Line line)
		{
			var lineP1 = new Point(line.X1, line.Y1) + CurrentOffsetVector;
			var lineP2 = new Point(line.X2, line.Y2) + CurrentOffsetVector;

			if (LinesIntersect(rect.TopLeft, rect.TopRight, lineP1, lineP2))
			{
				return true;
			}

			if (LinesIntersect(rect.TopRight, rect.BottomRight, lineP1, lineP2))
			{
				return true;
			}

			if (LinesIntersect(rect.BottomRight, rect.BottomLeft, lineP1, lineP2))
			{
				return true;
			}

			if (LinesIntersect(rect.BottomLeft, rect.TopLeft, lineP1, lineP2))
			{
				return true;
			}

			return false;
		}

		/// <param name="line1P1">Endpoint 1 of line 1</param>
		/// <param name="line1P2">Endpoint 2 of line 1</param>
		/// <param name="line2P1">Endpoint 1 of line 2</param>
		/// <param name="line2P2">Endpoint 2 of line 2</param>
		/// <returns></returns>
		private static bool LinesIntersect(Point line1P1, Point line1P2, Point line2P1, Point line2P2)
		{
			double
				s1_x = line1P2.X - line1P1.X,
				s1_y = line1P2.Y - line1P1.Y,

				s2_x = line2P2.X - line2P1.X,
				s2_y = line2P2.Y - line2P1.Y,

				s = (-s1_y * (line1P1.X - line2P1.X) + s1_x * (line1P1.Y - line2P1.Y)) / (-s2_x * s1_y + s1_x * s2_y),
				t = (s2_x * (line1P1.Y - line2P1.Y) - s2_y * (line1P1.X - line2P1.X)) / (-s2_x * s1_y + s1_x * s2_y);

			if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
			{
				return true;
			}

			return false;
		}

		private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Right)
			{
				InitialMousePosition = LayoutTransform.Inverse.Transform(e.GetPosition(this));
			}

			if (e.ChangedButton == MouseButton.Left)
			{
				if (e.Source is Zone zone && !zone.IsMirrorZone)
				{
					if (canvas.Children.Contains(zone))
					{
						if (!HighlightedElements.Contains(zone))
						{
							ClearHighlights();
							zone.BorderColor = Brushes.DodgerBlue;
							HighlightedElements.Add(zone);
						}

						SelectedElement = zone;

						var mousePosition = LayoutTransform.Inverse.Transform(e.GetPosition(this));
						var adjustedMousePos = new Point(mousePosition.X / CurrentScale, mousePosition.Y / CurrentScale);
						double x = Canvas.GetLeft(SelectedElement);
						double y = Canvas.GetTop(SelectedElement);

						//Debug.WriteLine($"elem left: {x}; elem top: {y}; mouse left: {adjustedMousePos.X}; mouse top: {adjustedMousePos.Y}");
						var elementPosition = new Point(x, y);

						DraggingDelta = (elementPosition - adjustedMousePos);
					}
					IsDraggingZone = true;
				}
				else
				{
					var line = LineClicked(sender, e);
					if (line != null)
					{
						if (HighlightedElements.Contains(line))
						{
							return;
						}

						ClearHighlights();

						var (zone1, zone2) = UIElementEndpointsDict[line];
						foreach (var endpointEntry in UIElementEndpointsDict)
						{
							if (endpointEntry.Key is not Line tmpLine)
							{
								continue;
							}

							if (endpointEntry.Value.Item1 == zone1 && endpointEntry.Value.Item2 == zone2)
							{
								tmpLine.Stroke = Brushes.DodgerBlue;
								HighlightedElements.Add(tmpLine);
							}
						}
					}
				}
			}
		}

		private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Right)
			{
				CurrentOffsetVector = TempVector / CurrentScale;

				
			}

			IsDraggingZone = false;
			SelectedElement = null;
		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (!IsDraggingZone && e.RightButton == MouseButtonState.Pressed)
			{
				Point mousePosition = LayoutTransform.Inverse.Transform(e.GetPosition(this));
				TempVector = Point.Subtract(mousePosition, InitialMousePosition) + CurrentOffsetVector * CurrentScale;
				//Debug.WriteLine($"Move - TempVector: {TempVector.ToString()}");
				MoveCanvasBy(TempVector);
			}

			if (IsDraggingZone && e.LeftButton == MouseButtonState.Pressed)
			{
				if (SelectedElement != null)
				{
					Point mousePosition = LayoutTransform.Inverse.Transform(e.GetPosition(this));

					double left = mousePosition.X / CurrentScale + DraggingDelta.X;
					var oldLeft = Canvas.GetLeft(SelectedElement);

					if (MirrorLeft != null && MirrorLeft < left + Constants.DefaultZoneWidth)
					{
						left = MirrorLeft.Value - Constants.DefaultZoneWidth;
					}

					//Debug.WriteLine($"Move - mousePosition.X: {mousePosition.X}; mousePos.X/CurrScale: {mousePosition.X / CurrentScale}; DraggingDelta.X: {DraggingDelta.X}");

					Canvas.SetLeft(SelectedElement, left);
					Canvas.SetTop(SelectedElement, mousePosition.Y / CurrentScale + DraggingDelta.Y);

					//Debug.WriteLine($"Move - Original ZoneLeft: {oldLeft} -> {left}");
					RepositionConnectedUIElements(SelectedElement);

					if (SelectedElement.MirrorZone != null)
					{
						var oldMirrorLeft = Canvas.GetLeft(SelectedElement.MirrorZone);
						var mirrorLeft = oldMirrorLeft + (oldLeft - left);
						Canvas.SetLeft(SelectedElement.MirrorZone, mirrorLeft);
						Canvas.SetTop(SelectedElement.MirrorZone, mousePosition.Y / CurrentScale + DraggingDelta.Y);

						//Debug.WriteLine($"Move - Mirror ZoneLeft: {oldMirrorLeft} -> {mirrorLeft}");

						RepositionConnectedUIElements(SelectedElement.MirrorZone);
					}
				}
			}
		}

		private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (e.Delta < 0)
			{
				CurrentScale *= Constants.ZoomOutMultiplier;
			}
			else
			{
				CurrentScale *= Constants.ZoomInMultiplier;
			}
		}

		private void RepositionConnectedUIElements(Zone zone)
		{
			if (!ConnectedUIElementsDict.TryGetValue(zone, out var elementLst))
			{
				return;
			}

			foreach (var element in elementLst)
			{
				var (zone1, zone2) = UIElementEndpointsDict[element];
				var metadata = UIElementMetadataDict[element];

				if (element is Label label)
				{
					AdjustLabelPosition(label, zone1, zone2, metadata.LinkCount, metadata.Index);
				}
				else if (element is Line line)
				{
					AdjustLinkPosition(line, zone1, zone2, metadata.LinkCount, metadata.Index);
				}
			}
		}

		private void MoveCanvasBy(Vector vector)
		{
			var translate = new TranslateTransform(vector.X / CurrentScale, vector.Y / CurrentScale);

			foreach (UIElement child in canvas.Children)
			{
				child.RenderTransform = translate;
			}
		}

		private void CenterTemplate()
		{
			if (ZoneUcDict.Count == 0)
			{
				return;
			}

			var enclosingRect = DetermineEnclosingRectangle(ZoneUcDict.Values);
			var templateDisplayWidth = enclosingRect.Width + 2 * Constants.TemplateBorderWidth;
			var templateDisplayHeight = enclosingRect.Height + 2 * Constants.TemplateBorderWidth;

			var scale = Math.Min(container.ActualWidth / templateDisplayWidth, container.ActualHeight / templateDisplayHeight);
			CurrentScale = scale;

			var moveX = (container.ActualWidth - templateDisplayWidth * scale) / 2 - enclosingRect.X * scale + Constants.TemplateBorderWidth*scale;
			var moveY = (container.ActualHeight - templateDisplayHeight * scale) / 2 - enclosingRect.Y * scale + Constants.TemplateBorderWidth*scale;
			TempVector = new Vector(moveX, moveY);

			MoveCanvasBy(TempVector);
			CurrentOffsetVector = TempVector / CurrentScale;
		}

		private void Rescale()
		{
			Matrix scaleMatrix = Matrix.Identity;
			scaleMatrix.ScaleAt(CurrentScale, CurrentScale, 0, 0);
			canvas.LayoutTransform = new MatrixTransform(scaleMatrix);
		}

		private void Redraw(Domain.RmgFormat.Template? template)
		{
			canvas.Children.Clear();
			MirrorLeft = null;
			IsMirrorTemplate = false;
			ZoneUcDict = [];

			if (template == null)
			{
				return;
			}

			IsMirrorTemplate = template.OwnerPack.Options.Mirror;
			
			var enclosingRect = DetermineEnclosingRectangle(template.Zones);

			Debug.WriteLine($"EnclosingRect width: {enclosingRect.Width}");
			Debug.WriteLine($"Hota scaling: {Constants.HotaScaling}");
			Debug.WriteLine($"Default zone width: {Constants.DefaultZoneWidth}");

			int maxPlayerNumber = template.Zones.Count == 0? 0: template.Zones.Max(z => z.PlayerTowns.Ownership);
			foreach (var zone in template.Zones)
			{
				var originalZoneLeft = zone.ImageSettings.X * Constants.HotaScaling;
				var originalZoneTop = zone.ImageSettings.Y * Constants.HotaScaling;

				var player = (zone.Type == ZoneType.HumanStart || zone.Type == ZoneType.ComputerStart) ? zone.PlayerTowns.Ownership : -1;

				var zoneuc = DrawZone(zone, zone.Id, originalZoneLeft, originalZoneTop);

				if (IsMirrorTemplate)
				{
					var mirrorZoneLeft = (enclosingRect.Left + 2 * enclosingRect.Width - (zone.ImageSettings.X - enclosingRect.Left) - Constants.DefaultZoneWidth-Constants.TemplateBorderWidth) * Constants.HotaScaling;
					var mirrorZoneTop = originalZoneTop;

					var zoneucm = DrawZone(zone, zone.Id + 100, mirrorZoneLeft, mirrorZoneTop, true);
					zoneuc.MirrorZone = zoneucm;
				}
			}

			foreach (var connection in template.Connections)
			{
				if (connection.IsMirrorConnection)
				{
					continue;
				}

				var zone1 = ZoneUcDict[connection.Zone1Id];
				var zone2 = ZoneUcDict[connection.Zone2Id];

				DrawConnection(connection, zone1, zone2);
			}

			if (IsMirrorTemplate)
			{
				foreach (var connection in template.Connections)
				{
					int zone1Id;
					int zone2Id;
					if (connection.IsMirrorConnection)
					{
						zone1Id = connection.Zone1Id;
						zone2Id = connection.Zone1Id + 100;
					}
					else
					{
						zone1Id = connection.Zone1Id + 100;
						zone2Id = connection.Zone2Id + 100;
					}

					var zone1 = ZoneUcDict[zone1Id];
					var zone2 = ZoneUcDict[zone2Id];

					DrawConnection(connection, zone1, zone2);
				}
			}

			if (IsMirrorTemplate)
			{
				DrawMirror();
			}

			CenterTemplate();
		}

		private Zone DrawZone(Domain.RmgFormat.Zone baseZone, int id, double left, double top, bool? isMirror = null)
		{
			var zoneuc = new Zone
			{
				ZoneId = id,
				Width = Constants.DefaultZoneWidth,
				Height = Constants.DefaultZoneHeight,
				MonsterStrength = baseZone.Monsters.Strength
			};

			var zvm = new ZoneViewModel(new DialogService(), baseZone);
			if (isMirror != null)
			{
				zvm.IsMirrorZone = isMirror.Value;
				zoneuc.IsMirrorZone = isMirror.Value;
			}

			ZoneUcDict.Add(zoneuc.ZoneId, zoneuc);
			zoneuc.DataContext = zvm;

			canvas.Children.Add(zoneuc);
			zoneuc.SetValue(Canvas.LeftProperty, left);
			zoneuc.SetValue(Canvas.TopProperty, top);

			return zoneuc;
		}

		private void DrawMirror()
		{
			double minLeft = double.MaxValue;
			double maxRight = double.MinValue;

			foreach (var zoneuc in ZoneUcDict.Values)
			{
				var val = Canvas.GetLeft(zoneuc);
				minLeft = Math.Min(minLeft, val);
				maxRight = Math.Max(maxRight, val + zoneuc.ActualWidth);
			}

			var mirrorOverlay = new Rectangle() { Width = 100000, Height = 100000, Fill = Brushes.Gray, Opacity = 0.4, StrokeThickness = 5, Stroke = Brushes.Black, StrokeDashArray = new DoubleCollection([3, 3]) };
			Panel.SetZIndex(mirrorOverlay, 2);
			canvas.Children.Add(mirrorOverlay);

			var mirrorLeft = (minLeft + maxRight) / 2 + Constants.DefaultZoneWidth / 2;
			MirrorLeft = mirrorLeft;


			Canvas.SetLeft(mirrorOverlay, mirrorLeft);
			Canvas.SetTop(mirrorOverlay, -50000);
		}

		private void DrawConnection(Connection connection, Zone zone1, Zone zone2)
		{
			var x1 = (double)zone1.GetValue(Canvas.LeftProperty) + Constants.DefaultZoneWidth / 2;
			var y1 = (double)zone1.GetValue(Canvas.TopProperty) + Constants.DefaultZoneHeight / 2;
			var x2 = (double)zone2.GetValue(Canvas.LeftProperty) + Constants.DefaultZoneWidth / 2;
			var y2 = (double)zone2.GetValue(Canvas.TopProperty) + Constants.DefaultZoneHeight / 2;

			for (int i = 0; i < connection.Links.Count; i++)
			{
				var link = connection.Links[i];

				var uiElements = CreateLink(link, connection.IsMirrorConnection);

				foreach (var uiElement in uiElements)
				{
					if (!ConnectedUIElementsDict.TryGetValue(zone1, out var lst))
					{
						lst = [];
						ConnectedUIElementsDict.Add(zone1, lst);
					}
					lst.Add(uiElement);

					if (!ConnectedUIElementsDict.TryGetValue(zone2, out lst))
					{
						lst = [];
						ConnectedUIElementsDict.Add(zone2, lst);
					}
					lst.Add(uiElement);

					UIElementEndpointsDict.Add(uiElement, (zone1, zone2));
					UIElementMetadataDict.Add(uiElement, new UIElementMetadata(i, connection.Links.Count));
				}

				foreach (var uiElement in uiElements)
				{
					if (uiElement is Label label)
					{
						AdjustLabelPosition(label, zone1, zone2, connection.Links.Count, i);
					}
					else if (uiElement is Line line)
					{
						AdjustLinkPosition(line, zone1, zone2, connection.Links.Count, i);
					}
				}
			}
		}

		private IReadOnlyList<UIElement> CreateLink(ConnectionLink link, bool isMirrorLink)
		{
			var result = new List<UIElement>();

			if (isMirrorLink)
			{
				result.Add(DrawLine("MirrorConnection"));
			}
			else if (link.Type == ConnectionType.Standard || link.Type == ConnectionType.BorderGuard)
			{
				if (link.Roads == ConnectionRoad.Yes)
				{
					result.Add(DrawLine("ForcedRoadConnectionBase", -2));
					result.Add(DrawLine("ForcedRoadConnectionTop", -1));
				}
				else if (link.Roads == ConnectionRoad.No)
				{
					result.Add(DrawLine("ForcedNoRoadConnection"));
				}
				else
				{
					result.Add(DrawLine("BasicConnection"));
				}
			}
			else if (link.Type == ConnectionType.Wide)
			{
				if (link.Roads == ConnectionRoad.No)
				{
					result.Add(DrawLine("WideConnectionNoRoad"));
				}
				else
				{
					result.Add(DrawLine("WideConnection"));
				}
			}
			else if (link.Type == ConnectionType.Fictive)
			{
				result.Add(DrawLine("FictiveConnection"));
			}
			else if (link.Restriction != null)
			{
				result.Add(DrawLine("ConditionalConnection"));
			}

			if (link.Value > 0)
			{
				var label = new Label
				{
					Content = link.Type == ConnectionType.BorderGuard ? "|" : link.Value / 1000,
					Padding = new Thickness(2, 0, 2, 0),
					Margin = new Thickness(),
					Background = Brushes.White,
					FontSize = 18,
					FontWeight = FontWeights.Bold,
					Foreground = Brushes.Red
				};

				label.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

				canvas.Children.Add(label);
				Panel.SetZIndex(label, -1);

				result.Add(label);
			}
			return result;
		}

		private static void AdjustLinkPosition(Line line, Zone zone1, Zone zone2, int linkCount, int linkIndex)
		{
			var x1 = (double)zone1.GetValue(Canvas.LeftProperty) + Constants.DefaultZoneWidth / 2;
			var y1 = (double)zone1.GetValue(Canvas.TopProperty) + Constants.DefaultZoneHeight / 2;
			var x2 = (double)zone2.GetValue(Canvas.LeftProperty) + Constants.DefaultZoneWidth / 2;
			var y2 = (double)zone2.GetValue(Canvas.TopProperty) + Constants.DefaultZoneHeight / 2;

			var nvx = y1 == y2 ? 1 : y1 - y2;
			var nvy = x2 == x1 ? 1 : x2 - x1;
			var nv = new Vector(nvx, nvy);
			nv.Normalize();

			var dx = nv.X * Constants.LineDistance;
			var dy = nv.Y * Constants.LineDistance;
			var num = linkCount;

			var xOffset = num % 2 == 1 ? 0 : dx / 2;
			var yOffset = num % 2 == 1 ? 0 : dy / 2;

			var startX1 = x1 - num / 2 * dx + xOffset;
			var startY1 = y1 - num / 2 * dy + yOffset;

			var startX2 = x2 - num / 2 * dx + xOffset;
			var startY2 = y2 - num / 2 * dy + yOffset;


			line.X1 = startX1 + dx * linkIndex;
			line.Y1 = startY1 + dy * linkIndex;
			line.X2 = startX2 + dx * linkIndex;
			line.Y2 = startY2 + dy * linkIndex;
		}

		private static void AdjustLabelPosition(Label label, Zone zone1, Zone zone2, int linkCount, int linkIndex)
		{
			var x1 = (double)zone1.GetValue(Canvas.LeftProperty) + Constants.DefaultZoneWidth / 2;
			var y1 = (double)zone1.GetValue(Canvas.TopProperty) + Constants.DefaultZoneHeight / 2;
			var x2 = (double)zone2.GetValue(Canvas.LeftProperty) + Constants.DefaultZoneWidth / 2;
			var y2 = (double)zone2.GetValue(Canvas.TopProperty) + Constants.DefaultZoneHeight / 2;

			var nvx = y1 == y2 ? 1 : y1 - y2;
			var nvy = x2 == x1 ? 1 : x2 - x1;
			var nv = new Vector(nvx, nvy);
			nv.Normalize();

			var dx = nv.X * Constants.LineDistance;
			var dy = nv.Y * Constants.LineDistance;
			var num = linkCount;

			var xOffset = num % 2 == 1 ? 0 : dx / 2;
			var yOffset = num % 2 == 1 ? 0 : dy / 2;

			var startX1 = x1 - num / 2 * dx + xOffset;
			var startY1 = y1 - num / 2 * dy + yOffset;

			var startX2 = x2 - num / 2 * dx + xOffset;
			var startY2 = y2 - num / 2 * dy + yOffset;

			var s1 = new Point(startX1 + dx * linkIndex, startY1 + dy * linkIndex);
			var s2 = new Point(startX2 + dx * linkIndex, startY2 + dy * linkIndex);

			var lineLength = Math.Abs((s1 - s2).Length);
			var distanceOffset = linkCount % 2 == 1 ? 0 : (label.DesiredSize.Height + label.DesiredSize.Width) / 4;
			var distanceFromEndPoint = lineLength / 2 + (linkIndex - linkCount / 2) * label.DesiredSize.Width + distanceOffset;
			var k = distanceFromEndPoint / lineLength;
			var left = k * s1.X + (1 - k) * s2.X - label.DesiredSize.Width / 2;
			var top = k * s1.Y + (1 - k) * s2.Y - label.DesiredSize.Height / 2;

			label.SetValue(Canvas.TopProperty, top);
			label.SetValue(Canvas.LeftProperty, left);
		}

		private Line DrawLine(string styleName, int zIndex = -1)
		{
			var line = new Line
			{
				Style = (Style)FindResource(styleName),
				Tag = styleName
			};

			Panel.SetZIndex(line, zIndex);
			canvas.Children.Add(line);

			return line;
		}

		private static Rect DetermineEnclosingRectangle(IEnumerable<Zone> zones)
		{
			double minX = double.MaxValue;
			double minY = double.MaxValue;
			double maxX = double.MinValue;
			double maxY = double.MinValue;

			foreach (var zone in zones)
			{
				var lowX = Canvas.GetLeft(zone);
				var highX = lowX + zone.Width;
				var lowY = Canvas.GetTop(zone);
				var highY = lowY + zone.Height;

				minX = lowX < minX ? lowX : minX;
				minY = lowY < minY ? lowY : minY;
				maxX = highX > maxX ? highX : maxX;
				maxY = highY > maxY ? highY : maxY;
			}

			return new Rect(minX, minY, maxX - minX, maxY - minY);
		}

		private static Rect DetermineEnclosingRectangle(IReadOnlyList<Domain.RmgFormat.Zone> zones)
		{
			if (zones.Count == 0)
			{
				return new Rect();
			}

			var minX = float.MaxValue;
			var minY = float.MaxValue;
			var maxX = float.MinValue;
			var maxY = float.MinValue;

			foreach (var zone in zones)
			{
				var x = zone.ImageSettings.X;
				var y = zone.ImageSettings.Y;

				minX = x < minX ? x : minX;
				minY = y < minY ? y : minY;
				maxX = x > maxX ? x : maxX;
				maxY = y > maxY ? y : maxY;
			}

			return new Rect(minX, minY, maxX - minX + Constants.DefaultZoneWidth, maxY - minY + Constants.DefaultZoneHeight);
		}

		private void EditConnectionMenuItem_Click(object sender, RoutedEventArgs e)
		{
			var window = new LinkSettings();
			window.ShowDialog();
		}
	}
}
