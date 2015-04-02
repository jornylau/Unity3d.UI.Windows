//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the UI.Windows Flow Addon.
//     You may simply edit this file to setup your behavior.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI.Windows;

namespace ExampleProject.UI.Other.MainLoader {

	public class MainLoaderScreenBase : LayoutWindowType {
		
		/// <summary>
		/// Flows to the GlobalMap.
		/// Full Name: ExampleProject.UI.Menu.GlobalMap.GlobalMapScreen
		/// </summary>
		/// <returns>GlobalMap</returns>
		public virtual ExampleProject.UI.Menu.GlobalMap.GlobalMapScreen FlowGlobalMap(params object[] parameters) {
			
			return WindowSystem.Show<ExampleProject.UI.Menu.GlobalMap.GlobalMapScreen>(parameters);
			
		}
				
		/// <summary>
		/// Flows to the Background.
		/// Full Name: ExampleProject.UI.Other.Background.BackgroundScreen
		/// </summary>
		/// <returns>Background</returns>
		public virtual ExampleProject.UI.Other.Background.BackgroundScreen FlowBackground(params object[] parameters) {
			
			return WindowSystem.Show<ExampleProject.UI.Other.Background.BackgroundScreen>(parameters);
			
		}
		
	}

}