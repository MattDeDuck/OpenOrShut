using BepInEx.Logging;
using PotionCraft.ManagersSystem;
using PotionCraft.NotificationSystem;
using PotionCraft.ObjectBased.UIElements.Tooltip;
using UnityEngine;

namespace OpenOrShut
{
    public static class Shop
    {
        static ManualLogSource Log => Plugin.Log;

        public static ShopButton shpButton;
        public static bool isOpen = true;

        public class Button
        {
            public static void Create()
            {
                shpButton = ShopButton.Create<ShopButton>(Utilities.shopHoverTex, Utilities.shopNormalTex, Button.Clicked, Button.ShopTooltip);
                var pinParentGO = GameObject.Find("Room Meeting").transform;
                shpButton.transform.parent = pinParentGO;
                shpButton.transform.localPosition = new Vector3(6.6f, -6.5f, 0f);
            }

            public static void Clicked()
            {
                if (isOpen)
                {
                    shpButton.normalSprite = Utilities.shopHoverSprite;
                    shpButton.hoveredSprite = Utilities.shopNormalSprite;

                    Notification.ShowText(Text.ShopTitle, Text.ShopClosedText, Notification.TextType.EventText);

                    Managers.Debug.SkipAllNpc();

                    Log.LogInfo("Shop is now closed");
                }
                else
                {
                    shpButton.normalSprite = Utilities.shopNormalSprite;
                    shpButton.hoveredSprite = Utilities.shopHoverSprite;

                    Notification.ShowText(Text.ShopTitle, Text.ShopOpenText, Notification.TextType.EventText);

                    Managers.Npc.SpawnNpcOnDayStart();

                    Log.LogInfo("Shop is now open");
                }

                isOpen = !isOpen;
            }

            public static TooltipContent ShopTooltip()
            {
                if (isOpen)
                {
                    return new TooltipContent
                    {
                        header = "Close your shop?"
                    };
                }
                else
                {
                    return new TooltipContent
                    {
                        header = "Open your shop?"
                    };
                }
            }

            public class Text
            {
                public static string ShopTitle
                {
                    get
                    {
                        return "Your Alchemy Shop";
                    }
                }

                public static string ShopOpenText
                {
                    get
                    {
                        return "Shop is now open! Customers and traders are now on their way!";
                    }
                }

                public static string ShopClosedText
                {
                    get
                    {
                        return "Shop is now closed! Feel free to explore the map and brew those potions!";
                    }
                }
            }
        }
    }
}
