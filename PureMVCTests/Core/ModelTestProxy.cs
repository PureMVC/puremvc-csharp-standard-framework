//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using PureMVC.Patterns.Proxy;

namespace PureMVC.Core
{
    public class ModelTestProxy: Proxy
    {
        public new const string NAME = "ModelTestProxy";
        public const string ON_REGISTER_CALLED = "onRegister Called";
        public const string ON_REMOVE_CALLED = "onRemove Called";

        public ModelTestProxy() : base(NAME, "")
        {
        }

        public override void OnRegister()
        {
            Data = ON_REGISTER_CALLED;
        }

        public override void OnRemove()
        {
            Data = ON_REMOVE_CALLED;
        }
    }
}
