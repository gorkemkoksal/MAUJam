using Core._Common.Helpers;
using UnityEngine;

namespace Popup
{
	/// <summary>
	/// Base popup component.
	/// </summary>
	[RequireComponent(typeof(CanvasGroup))]
	public class Popup : BaseComposite<CanvasGroup>
	{
		/// <summary>
		/// Discardable means clicking on blackout closes it. Like settings window. Save conflict is not discardable because it needs to resolve.
		/// </summary>
		public bool isDiscardable = true; 
		public CanvasGroup Canvas => Target;
	}
}