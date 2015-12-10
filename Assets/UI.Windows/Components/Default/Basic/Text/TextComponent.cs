﻿using UnityEngine;
using System;
using System.Text.RegularExpressions;

namespace UnityEngine.UI.Windows.Components {

	public class TextComponent : ColoredComponent, ITextComponent {

		public enum RichTextFlags : byte {

			None = 0x0,
			Color = 0x1,
			Bold = 0x2,
			Italic = 0x4,
			Size = 0x8,
			Material = 0x10,
			Quad = 0x20,

		};

		public override void Setup(IComponentParameters parameters) {
			
			base.Setup(parameters);
			
			var inputParameters = parameters as TextComponentParameters;
			#region source macros UI.Windows.Initialization.TextComponent
			{
				if (inputParameters != null) inputParameters.Setup(this as ITextComponent);
			}
			#endregion

		}

		#region source macros UI.Windows.TextComponent
		[Header("Text Component")]
		[SerializeField]
		private Text text;
		public UnityEngine.UI.Windows.Components.TextComponent.ValueFormat valueFormat;
		[BitMask(typeof(RichTextFlags))]
		public RichTextFlags richTextFlags = RichTextFlags.Color | RichTextFlags.Bold | RichTextFlags.Italic | RichTextFlags.Size | RichTextFlags.Material | RichTextFlags.Quad;

		public void SetBestFit(bool state, int minSize = 10, int maxSize = 40) {
			
			if (this.text != null) {
				
				this.text.resizeTextForBestFit = state;
				this.text.resizeTextMinSize = minSize;
				this.text.resizeTextMaxSize = maxSize;
				
			}
			
		}
		
		public void SetBestFitState(bool state) {
			
			if (this.text != null) this.text.resizeTextForBestFit = state;
			
		}
		
		public void SetBestFitMinSize(int size) {
			
			if (this.text != null) this.text.resizeTextMinSize = size;
			
		}
		
		public void SetBestFitMaxSize(int size) {
			
			if (this.text != null) this.text.resizeTextMaxSize = size;
			
		}

		public void SetFont(Font font) {
			
			if (this.text != null) this.text.font = font;
			
		}

		public void SetFontSize(int fontSize) {
			
			if (this.text != null) this.text.fontSize = fontSize;
			
		}

		public void SetLineSpacing(float value) {
			
			if (this.text != null) this.text.lineSpacing = value;
			
		}
		
		public void SetRichText(bool state) {
			
			if (this.text != null) this.text.supportRichText = state;
			
		}
		
		public void SetFontStyle(FontStyle fontStyle) {
			
			if (this.text != null) this.text.fontStyle = fontStyle;
			
		}
		
		public float GetContentHeight(float heightPadding = 0f) {
			
			if (this.text == null) return 0f;
			
			return this.GetContentHeight(this.GetText(), heightPadding);
			
		}
		
		public float GetContentHeight(string text, float heightPadding = 0f) {
			
			if (this.text == null) return 0f;
			
			return this.GetContentHeight(text, (this.transform.root as RectTransform).rect.size) + heightPadding;
			
		}

		public float GetContentHeight(string text, Vector2 containerSize) {

			if (this.text == null) return 0f;

			var settings = this.text.GetGenerationSettings(containerSize);
			return this.text.cachedTextGenerator.GetPreferredHeight(text, settings);

		}

		public void SetValueFormat(UnityEngine.UI.Windows.Components.TextComponent.ValueFormat format) {

			this.valueFormat = format;

		}
		
		public void SetValue(long value) {
			
			this.SetValue(value, this.valueFormat);
			
		}
		
		public void SetValue(int value) {
			
			this.SetValue(value, this.valueFormat);

		}

		public void SetValue(long value, UnityEngine.UI.Windows.Components.TextComponent.ValueFormat format) {
			
			this.SetText(TextComponent.FormatValue(value, format));
			
		}

		public void SetValue(int value, UnityEngine.UI.Windows.Components.TextComponent.ValueFormat format) {
			
			this.SetText(TextComponent.FormatValue(value, format));
			
		}

		public void SetTextVerticalOverflow(VerticalWrapMode mode) {
			
			if (this.text != null) this.text.verticalOverflow = mode;
			
		}
		
		public void SetTextHorizontalOverflow(HorizontalWrapMode mode) {
			
			if (this.text != null) this.text.horizontalOverflow = mode;
			
		}

		public void SetTextAlignment(TextAnchor anchor) {
			
			if (this.text != null) this.text.alignment = anchor;
			
		}

		public string GetText() {

			return (this.text != null) ? this.text.text : string.Empty;

		}

		public void SetText(string text) {

			if (this.text != null) {

				if (this.text.supportRichText == true) {

					text = TextComponent.ParseRichText(text, this.richTextFlags);

				}

				this.text.text = text;

			}

		}

		public virtual void SetTextAlpha(float value) {

			var color = this.GetTextColor();
			color.a = value;
			this.SetTextColor(color);

		}

		public virtual void SetTextColor(Color color) {
			
			if (this.text != null) this.text.color = color;
			
		}
		
		public virtual Color GetTextColor() {

			if (this.text == null) return Color.white;

			return this.text.color;
			
		}
		#endregion
		
		#if UNITY_EDITOR
		public override void OnValidateEditor() {
			
			base.OnValidateEditor();
			
			if (this.gameObject.activeSelf == false) return;
			
			#region source macros UI.Windows.Editor.TextComponent
			var texts = this.GetComponentsInChildren<Text>(true);
			if (texts.Length == 1) this.text = texts[0];

			if (this.valueFormat != ValueFormat.None) {

				this.SetValue(999999L);

			}
			#endregion
			
		}
		#endif

		public static string ParseRichText(string text, RichTextFlags flags) {
			
			if ((flags & RichTextFlags.Bold) == 0) {
				
				text = Regex.Replace(text, @"<b>", String.Empty);
				text = Regex.Replace(text, @"</b>", String.Empty);
				
			}
			
			if ((flags & RichTextFlags.Italic) == 0) {
				
				text = Regex.Replace(text, @"<i>", String.Empty);
				text = Regex.Replace(text, @"</i>", String.Empty);
				
			}
			
			if ((flags & RichTextFlags.Size) == 0) {
				
				text = Regex.Replace(text, @"<size=[0-9]+>", String.Empty);
				text = Regex.Replace(text, @"</size>", String.Empty);
				
			}
			
			if ((flags & RichTextFlags.Color) == 0) {
				
				text = Regex.Replace(text, @"<color=[^>]+>", String.Empty);
				text = Regex.Replace(text, @"</color>", String.Empty);
				
			}
			
			if ((flags & RichTextFlags.Material) == 0) {
				
				text = Regex.Replace(text, @"<material=[^>]+>", String.Empty);
				text = Regex.Replace(text, @"</material>", String.Empty);
				
			}
			
			if ((flags & RichTextFlags.Quad) == 0) {
				
				text = Regex.Replace(text, @"<quad [^>]+>", String.Empty);
				
			}

			return text;

		}

		public enum ValueFormat : byte {
			
			None,		 // 1234567890
			WithSpace,	 // 1 234 567 890
			WithComma,	 // 1,234 567 890
			TimeHMSFromSeconds,				// 00:00:00
			TimeMSFromSeconds,				// 00:00
			TimeHMSmsFromMilliseconds,		// 00:00:00`00
			TimeMSmsFromMilliseconds,		// 00:00`00

			DateDMHMS,						// 12 Aug 00:00:00
			DateDMHMSFromMilliseconds,
			TimeMSFromMilliseconds,			// 00:00

		};
		
		public static string FormatValue(int value, ValueFormat format) {
			
			return TextComponent.FormatValue((double)value, format);
			
		}
		
		public static string FormatValue(long value, ValueFormat format) {
			
			return TextComponent.FormatValue((double)value, format);
			
		}

		public static string FormatValue(double value, ValueFormat format) {
			
			var output = string.Empty;
			
			switch (format) {
				
				case ValueFormat.None: {
					
					output = value.ToString();
					
					break;

				}
				
				case ValueFormat.WithSpace: {
					
					if (value < 0f) {
						
						output = string.Format("-{0}", (-value).ToString("# ### ### ##0").Trim());
						
					} else {
						
						output = value.ToString("# ### ### ##0").Trim();
						
					}
					
					break;

				}
				
				case ValueFormat.WithComma: {
					
					if (value < 0f) {
						
						output = string.Format("-{0}", (-value).ToString("#,### ### ##0").Trim(','));
						
					} else {
						
						output = value.ToString("#,### ### ##0").Trim(',').Trim(' ');
						
					}
					
					break;

				}
				
				case ValueFormat.DateDMHMS: {

					DateTime date = new DateTime((long)value);
					output = date.ToString("dd MM hh:mm:ss");

					break;

				}

				case ValueFormat.TimeHMSFromSeconds: {

					var t = TimeSpan.FromSeconds(value);
						output = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
					
					break;

				}
				
				case ValueFormat.TimeMSFromSeconds: {

					var t = TimeSpan.FromSeconds(value);
						output = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
					
					break;

				}
					
				case ValueFormat.DateDMHMSFromMilliseconds: {
					
					DateTime date = new DateTime((long)(value / 1000d));
					output = date.ToString("dd MM hh:mm:ss");
					
					break;
					
				}

				case ValueFormat.TimeHMSmsFromMilliseconds: {

					var t = TimeSpan.FromMilliseconds(value);
						output = string.Format("{0:D2}:{1:D2}:{2:D2}`{3:D2}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
					
					break;

				}
				
				case ValueFormat.TimeMSmsFromMilliseconds: {

					var t = TimeSpan.FromMilliseconds(value);
						output = string.Format("{0:D2}:{1:D2}`{2:D2}", t.Minutes, t.Seconds, t.Milliseconds);
					
					break;

				}

				case ValueFormat.TimeMSFromMilliseconds: {
					
					var t = TimeSpan.FromMilliseconds(value);
					output = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);

					break;

				}
				
			}
			
			return output;
			
		}

	}

}