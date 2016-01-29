using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ListView
{
    public class ListItem
    {

        public const int TypeText = 0;
        public const int TypeButtonAdd = 1;
        public const int TypeButtonEdit = 2;
        public const int TypeButtonDelete = 3;
        public const int TypeToggle = 4;

        private int num;
        private string label;
        private int type;

        /// <summary>
        /// Create Default List Item
        /// </summary>
        /// <param name="num">Order in list (not implemented yet)</param>
        /// <param name="label">Text to show for label</param>
        public ListItem(int num, string label)
        {
            this.num = num;
            this.label = label;
            this.type = 0;
        }

        /// <summary>
        /// Create List Item as button
        /// 0 = Text 1-3 = Button 4 = Toggle
        /// </summary>
        /// <param name="num">Order in list (not implemented yet)</param>
        /// <param name="label">Text to show for label</param>
        /// <param name="buttonType">Button Type from ListItem.Type</param>
        public ListItem(int num, string label, int operationType)
        {
            this.num = num;
            this.label = label;
            this.type = operationType;
        }

        public int GetNum()
        {
            return num;
        }
        public string GetLabel()
        {
            return label;
        }
        public bool IsButton()
        {
            if (type > 0) return true;
            else return false;
        }
        public int GetType()
        {
            if (IsButton())
            {
                return type;
            }
            else
            {
                return 0;
            }
        }
    }
}
