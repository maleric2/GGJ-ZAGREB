using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ListView
{
    /// <summary>
    /// Predefinirane metode za ListView (abstract)
    /// Implementirati konkretne događaje na klik
    /// Implementirati konverziju određenog objekta
    /// </summary>
    public abstract class ListTableView<T> : MonoBehaviour
    {
        [Header("References on Current List")]
        public GameObject itemsReference;
        public GameObject headerReference;
        [Space(15)]

        [Header("Empty Containers of Header Item and List Element used to spawn elements")]
        public GameObject headerPrefab;
        public GameObject listElementPrefab;

        [Header("Prefabs of type of elements ")]
        public GameObject textType;
        public GameObject toggleType;
        public GameObject buttonType;
        public GameObject buttonEditType;
        public GameObject buttonDeleteType;

        public List<String> headerNames;
        ListTableController<T> controller;

        
        // Use this for initialization
        void Start()
        {
            controller = new ListTableController<T>();
            if(buttonEditType!=null && buttonDeleteType!=null && toggleType!=null && buttonType!=null)
                controller.Initialize(headerReference, itemsReference, headerPrefab, listElementPrefab, headerNames, textType, toggleType, buttonType, buttonEditType, buttonDeleteType);
            else if(toggleType!=null && buttonType!=null) 
                controller.Initialize(headerReference, itemsReference, headerPrefab, listElementPrefab, headerNames, textType, toggleType, buttonType);
            else 
                controller.Initialize(headerReference, itemsReference, headerPrefab, listElementPrefab, textType);
        }

        /// <summary>
        /// Dodaje novi Element u listu. 
        /// Pretvara objekt "element" prema ConvertToListRow
        /// (Template Method)
        /// </summary>
        /// <param name="element"></param>
        public void AddNewElement(T element)
        {
            if (controller != null)
            {
                ListRow<T> newItem = ConvertToListRow(element);
                if (newItem != null) controller.CreateNewItem(newItem);
            }
        }
        public void RemoveElement(ListRow<T> row)
        {
            Debug.Log("ListTableView - Brisem zapis sa id " + row.Id);
            controller.Remove(row);
        }
        public void RemoveAll()
        {
            Debug.Log("ListTableView - Brisem sve");
            controller.RemoveAll();
        }

        /// <summary>
        /// Pretvorba objekta element u objekt ListRow
        /// Inizijalizirati: ListRow.Initialize(element, id)
        /// Dodati elemente reda: ListRow.AddItem(id, label, [tip])
        /// Tip može biti ListItem.Type (Gumb, Toggle, Text)
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public abstract ListRow<T> ConvertToListRow(T element);

        void OnEnable()
        {
            ListTableController<T>.OnButtonPressed += RegisterButton;
        }

        private void RegisterButton(int buttonType, ListRow<T> item)
        {
            if (buttonType == ListItem.TypeButtonDelete) OnDeleteButtonClick(item);
            if (buttonType == ListItem.TypeButtonEdit) OnEditButtonClick(item);
            if (buttonType == ListItem.TypeButtonAdd) OnAddButtonClick(item);
            if (buttonType == ListItem.TypeToggle) OnSelectedRow(item);
        }

        #region Events
        /// <summary>
        /// Događaj na klik gumba Edit
        /// </summary>
        /// <param name="item"></param>
        public abstract void OnEditButtonClick(ListRow<T> item);

        /// <summary>
        /// Događaj na klik gumba Delete
        /// </summary>
        /// <param name="item"></param>
        public abstract void OnDeleteButtonClick(ListRow<T> item);

        /// <summary>
        /// Događaj na klik gumba Add
        /// </summary>
        /// <param name="item"></param>
        public abstract void OnAddButtonClick(ListRow<T> item);

        /// <summary>
        /// Događaj kada se selektira red
        /// </summary>
        /// <param name="item"></param>
        public abstract void OnSelectedRow(ListRow<T> item);

        public void OnSelectAll(bool select)
        {
            controller.SetToggleAll(select);
        }
        #endregion

    }
}
