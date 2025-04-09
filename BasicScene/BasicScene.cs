using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace Nez.Samples
{
    internal class BasicScene : Scene, IFinalRenderDelegate
    {
        public const string Moon = @"Content/Shared/moon.png";
        public const int ScreenSpaceRenderLayer = 999;
		ScreenSpaceRenderer _screenSpaceRenderer;
        private Scene _scene;

        public BasicScene()
        {
				AddRenderer(new ScreenSpaceRenderer(100, ScreenSpaceRenderLayer));

        }

        public override void Initialize()
        {
            base.Initialize();

            // default to 1280x720 with no SceneResolutionPolicy
            SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
            Screen.SetSize(1280, 720);

            var moonTex = Content.Load<Texture2D>("moon");
            //var moonTex = Content.LoadTexture(Nez.Content.Moon);
            var playerEntity = CreateEntity("player", new Vector2(Screen.Width / 2, Screen.Height / 2));
            playerEntity.AddComponent(new SpriteRenderer(moonTex));
        }

        public void OnAddedToScene(Scene scene) => _scene = scene;

        public void OnSceneBackBufferSizeChanged(int newWidth, int newHeight) => _screenSpaceRenderer.OnSceneBackBufferSizeChanged(newWidth, newHeight);

        public void HandleFinalRender(RenderTarget2D finalRenderTarget, Color letterboxColor, RenderTarget2D source,
                                      Rectangle finalRenderDestinationRect, SamplerState samplerState)
        {
            Core.GraphicsDevice.SetRenderTarget(null);
            Core.GraphicsDevice.Clear(letterboxColor);
            Graphics.Instance.Batcher.Begin(BlendState.Opaque, samplerState, DepthStencilState.None, RasterizerState.CullNone, null);
            Graphics.Instance.Batcher.Draw(source, finalRenderDestinationRect, Color.White);
            Graphics.Instance.Batcher.End();

            _screenSpaceRenderer.Render(_scene);
        }
    }
}
