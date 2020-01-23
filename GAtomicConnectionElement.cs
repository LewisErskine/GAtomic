using System;

namespace GLDoors2.GAtomic.objects
{
	/// <summary>
	/// Summary description for GAtomicConnectionElement.
	/// </summary>
	public class GAtomicConnectionElement
	{
		#region VARIABLES
		private GAtomicElementObject atomic_element;
		public GAtomicElementObject AtomicElement
		{
			get
			{	return atomic_element;	}
			set
			{	atomic_element = value;	}
		}
		/// <summary>
		/// Direction with respect to the connecting element.
		/// Eg: Connection element is on the right (therefore should have a left direction), therefore left
		/// </summary>
		private GAtomicConnectionDirection direction_of_connecting_element;
		public GAtomicConnectionDirection DirectionOfConnectingElement
		{
			get
			{	return direction_of_connecting_element;	}
		}
		#endregion

		#region CONSTRUCTOR
		public GAtomicConnectionElement(GAtomicElementObject elem, GAtomicConnectionDirection dir)
		{
			atomic_element = elem;
			direction_of_connecting_element = dir;
		}
		#endregion
	}
}
