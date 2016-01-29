using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ListView
{
    /// <summary>
    /// Primjer klase koja prima samo stringove
    /// </summary>
    public class StringListTableView : ListTableView<List<String>>
    {

        public override ListRow<List<string>> ConvertToListRow(List<string> element)
        {
            ListRow<List<String>> newListRow = new ListRow<List<String>>();
            newListRow.Initialize(element, 0);

            //Dodajemo Listu stringova kao pojedini element reda
            foreach(String s in element){
                newListRow.AddItem(new ListItem(0,s));
            }
            //Dodajemo Toggle (checkbox)
            newListRow.AddItem(new ListItem(0, "", ListItem.TypeToggle));

            return newListRow;
        }

        public override void OnEditButtonClick(ListRow<List<string>> item)
        {
            throw new NotImplementedException();
        }

        public override void OnDeleteButtonClick(ListRow<List<string>> item)
        {
            throw new NotImplementedException();
        }

        public override void OnAddButtonClick(ListRow<List<string>> item)
        {
            throw new NotImplementedException();
        }

        public override void OnSelectedRow(ListRow<List<string>> item)
        {
            throw new NotImplementedException();
        }
    }
}
