using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Wobble.Graphics;
using Wobble.Graphics.Sprites;

namespace Quaver.Graphics
{
    public abstract class HeaderedContainer : Sprite
    {
        /// <summary>
        ///     The header background for the text
        /// </summary>
        protected Sprite Header { get; set; }

        /// <summary>
        ///     The text in the header.
        /// </summary>
        protected SpriteText HeaderText { get; set; }

        /// <summary>
        ///     The size of the content container.
        /// </summary>
        protected Vector2 ContentSize => new Vector2(Width, Height - Header.Height);

        /// <summary>
        ///     The content of
        /// </summary>
        protected Sprite Content { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Ctor -
        /// </summary>
        /// <param name="size"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="textScale"></param>
        /// <param name="headerTextAlignment"></param>
        /// <param name="headerHeight"></param>
        /// <param name="headerColor"></param>
        internal HeaderedContainer(Vector2 size, string text, SpriteFont font, float textScale,
            Alignment headerTextAlignment, float headerHeight, Color headerColor)
        {
            Size = new ScalableVector2(size.X, size.Y);
            Alpha = 0;

            Header = new Sprite
            {
                Parent = this,
                Size = new ScalableVector2(Width, headerHeight),
                Tint = headerColor
            };

            HeaderText = new SpriteText(font, text)
            {
                Parent = Header,
                TextScale = textScale,
                Alignment = headerTextAlignment,
                TextAlignment = headerTextAlignment
            };
        }

        /// <summary>
        ///     Creates the sprite for the actual content.
        /// </summary>
        /// <returns></returns>
        protected abstract Sprite CreateContent();
    }
}
