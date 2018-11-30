using System.Collections.Generic;
using UnityEngine;

// https://garry.tv/2015/06/14/unity-tips/

namespace Soraphis {
	
	public abstract class ListBehaviour<T> : MonoBehaviour where T : MonoBehaviour{

		public static List<T> Instances = new List<T>(); 
		public static List<T> ActiveInstances = new List<T>();

		private bool quitting; 
	
		protected virtual void Start() {
			Instances.Add( this as T );
		}

		private void OnApplicationQuit() { quitting = true; }
	
		private void OnDestroy() { 
			if(!quitting) Instances.Remove( this as T );
		}

		protected virtual void OnEnable(){
			ActiveInstances.Add( this as T );
		}

		protected virtual void OnDisable(){
			ActiveInstances.Remove( this as T );
		}
	
	
	}
}
