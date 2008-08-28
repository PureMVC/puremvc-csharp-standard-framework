/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections.Generic;

using NUnitLite;
using NUnit.Framework;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.proxy;

namespace org.puremvc.csharp.core
{
	public class ModelTestProxy : Proxy
	{
		public new const String NAME = "ModelTestProxy";
		public const String ON_REGISTER_CALLED = "onRegister Called";
		public const String ON_REMOVE_CALLED = "onRemove Called";

		public ModelTestProxy()
			: base(NAME, "")
		{
		}

		public override void onRegister()
		{
			setData(ON_REGISTER_CALLED);
		}		

		public override void onRemove()
		{
			setData(ON_REMOVE_CALLED);
		}		
	}
}