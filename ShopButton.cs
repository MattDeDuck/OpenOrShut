using BepInEx.Logging;
using PotionCraft.ObjectBased.UIElements;
using PotionCraft.ObjectBased.UIElements.Tooltip;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace OpenOrShut
{
    public class ShopButton : SpriteChangingButton
    {
        static ManualLogSource Log => Plugin.Log;

        public static T Create<T>(Texture2D hover, Texture2D idle, Action onRelease, Func<TooltipContent> getTooltip) where T : ShopButton
        {
            GameObject obj = new(typeof(T).Name)
            {
                layer = LayerMask.NameToLayer("UI")
            };

            var button = obj.AddComponent<T>();
            button.spriteRenderer = obj.AddComponent<SpriteRenderer>();
            button.thisCollider = obj.AddComponent<BoxCollider2D>();
            button.sortingGroup = obj.AddComponent<SortingGroup>();
            button.tooltipContentProvider = obj.AddComponent<TooltipContentProvider>();

            Sprite sprHover = Sprite.Create(hover, new Rect(0, 0, hover.width, hover.height), new Vector2(0.5f, 0.5f));
            button.hoveredSprite = sprHover;
            button.pressedSprite = sprHover;

            Sprite sprIdle = Sprite.Create(idle, new Rect(0, 0, hover.width, hover.height), new Vector2(0.5f, 0.5f));
            button.lockedSprite = sprIdle;
            button.normalSprite = sprIdle;

            button.hoveredSprites = new Sprite[0];
            button.pressedSprites = new Sprite[0];
            button.lockedSprites = new Sprite[0];
            button.normalSprites = new Sprite[0];

            button.IgnoreRotationForPivot = true;
            button.showOnlyFingerWhenInteracting = true;

            button.spriteRenderer.sprite = button.normalSprite;
            (button.thisCollider as BoxCollider2D).size = button.spriteRenderer.sprite.bounds.size;

            button.sortingGroup.sortingLayerID = -1758066705;
            button.sortingGroup.sortingLayerName = "GuiBackground";

            button.tooltipContentProvider.fadingType = TooltipContentProvider.FadingType.UIElement;
            button.tooltipContentProvider.positioningSettings = new List<PositioningSettings>()
            {
                new PositioningSettings()
                {
                    bindingPoint = PositioningSettings.BindingPoint.TransformPosition,
                    freezeX = true,
                    freezeY = true,
                    position = new Vector2(0.4f, 0.4f),
                    tooltipCorner = PositioningSettings.TooltipCorner.LeftTop
                }
            };
            button.tooltipContentProvider.tooltipCollider = button.thisCollider;

            button._onRelease = onRelease;
            button._getTooltip = getTooltip;

            obj.SetActive(true);
            return button;
        }

        private protected Action _onRelease;
        public Func<TooltipContent> _getTooltip;

        public override void Start()
        {
            Log.LogInfo($"{GetType().Name} started");
            base.Start();
        }

        public override void OnButtonReleasedPointerInside()
        {
            base.OnButtonReleasedPointerInside();
            _onRelease?.Invoke();
        }

        public override TooltipContent GetTooltipContent()
        {
            return _getTooltip?.Invoke();
        }

        
    }
}
