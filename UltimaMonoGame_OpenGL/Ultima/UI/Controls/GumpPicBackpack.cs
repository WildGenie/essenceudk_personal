﻿using UltimaXNA.Ultima.Entities.Items;

namespace UltimaXNA.Ultima.UI.Controls
{
    class GumpPicBackpack : GumpPic
    {
        public Item BackpackItem
        {
            get;
            protected set;
        }

        public GumpPicBackpack(AControl owner, int page, int x, int y, Item backpack)
            : base(owner, page, x, y, 0xC4F6, 0)
        {
            BackpackItem = backpack;
        }
    }
}
