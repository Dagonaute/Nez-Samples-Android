using Android.Service.Voice;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nez.Samples
{
    public class Game1 : Nez.Core 
    {
        protected override void Initialize()
        {
            base.Initialize();
			Scene = new BasicScene();
        }
    }
}
