using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ListView
{
    //TODO pretvorit da ovo nije T da prihvaća više vrsta od jednom
    public class ListTableController<T>
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

        protected List<String> headerNames;


        //Lista instanciranih headera TODO: ovo još razdvojit
        protected List<GameObject> headersObject;

        public delegate void ListTouchEvent(int buttonType, ListRow<T> item);
        public static event ListTouchEvent OnButtonPressed;
        //public static event ListTouchEvent ;

        //Lista elemenata ListTable-a
        protected List<ListRow<T>> items;

        public void Initialize(GameObject headerReference, GameObject itemsReference, GameObject headerPrefab, GameObject listElementPrefab, GameObject textType)
        {
            Debug.Log("Inicijalizacija liste (prva varijanta)");
            items = new List<ListRow<T>>();
            this.itemsReference = itemsReference;
            this.headerReference = headerReference;
            this.textType = textType;
            this.headerPrefab = headerPrefab;
            this.listElementPrefab = listElementPrefab;


        }
        public void Initialize(GameObject headerReference, GameObject itemsReference, GameObject headerPrefab, GameObject listElementPrefab, List<String> headerNames, GameObject textType, GameObject toggleType, GameObject buttonType)
        {
            Debug.Log("Inicijalizacija liste (druga varijanta)");
            items = new List<ListRow<T>>();
            this.itemsReference = itemsReference;
            this.headerReference = headerReference;
            this.headerPrefab = headerPrefab;
            this.listElementPrefab = listElementPrefab;
            this.headerNames = headerNames;
            this.textType = textType;
            this.toggleType = toggleType;
            this.buttonType = buttonType;

            if (headerNames != null) SetHeaders(headerNames);

        }
        public void Initialize(GameObject headerReference, GameObject itemsReference, GameObject headerPrefab, GameObject listElementPrefab, List<String> headerNames, GameObject textType, GameObject toggleType, GameObject buttonType, GameObject buttonEditType, GameObject buttonDeleteType)
        {
            Debug.Log("Inicijalizacija liste (druga varijanta)");
            items = new List<ListRow<T>>();
            this.itemsReference = itemsReference;
            this.headerReference = headerReference;
            this.headerPrefab = headerPrefab;
            this.listElementPrefab = listElementPrefab;
            this.headerNames = headerNames;
            this.textType = textType;
            this.toggleType = toggleType;
            this.buttonType = buttonType;
            this.buttonEditType = buttonEditType;
            this.buttonDeleteType = buttonDeleteType;

            if (headerNames != null) SetHeaders(headerNames);

        }
        public bool IsInitialized()
        {
            if (headerReference != null && itemsReference != null && headerPrefab != null && listElementPrefab != null && textType != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SetHeaders(List<String> headerNames)
        {
            this.headerNames = headerNames;
            if (headerReference == null) return;

            foreach (String header in headerNames)
            {
                GameObject h = GameObject.Instantiate(headerPrefab);
                h.GetComponent<Text>().text = header;
                h.gameObject.transform.SetParent(headerReference.transform);
                if (headersObject == null) headersObject = new List<GameObject>();
                headersObject.Add(h);
            }
        }


        internal GameObject CreateNewItem(ListRow<T> elem)
        {
            Debug.Log("Kreiram novi list redak");
            GameObject newList = GameObject.Instantiate(listElementPrefab);
            foreach (ListItem l in elem.GetItems())
            {
                if (l != null)
                {
                    GameObject newListItem = null;
                    if (l.GetType() == ListItem.TypeText)
                    {
                        newListItem = CreateTextItem(l);
                    }
                    else if (l.GetType() == ListItem.TypeButtonAdd || l.GetType() == ListItem.TypeButtonDelete || l.GetType() == ListItem.TypeButtonEdit)
                    {
                        newListItem = CreateButtonItem(l, elem);
                    }
                    else if (l.GetType() == ListItem.TypeToggle)
                    {
                        newListItem = CreateToggleItem(l, elem);

                    }
                    if (newListItem != null) newListItem.transform.SetParent(newList.transform);
                }
            }
            if (newList != null)
            {
                elem.GameObject = newList;
                newList.transform.SetParent(itemsReference.transform);
                items.Add(elem);
                return newList;
            }

            return null;
        }

        private GameObject CreateTextItem(ListItem item)
        {
            if (textType != null || textType.GetComponent<Text>() != null)
            {
                GameObject newTextItem = GameObject.Instantiate(textType);
                newTextItem.GetComponent<Text>().text = item.GetLabel();
                return newTextItem;
            }
            else
            {
                return null;
            }
        }

        private GameObject CreateButtonItem(ListItem item, ListRow<T> row)
        {
            if (buttonType != null || buttonType.GetComponent<Button>() != null)
            {
                GameObject newButtonItem =null;
                if (item.GetType() == ListItem.TypeButtonEdit && buttonEditType != null) newButtonItem = GameObject.Instantiate(buttonEditType);
                else if (item.GetType() == ListItem.TypeButtonDelete && buttonDeleteType != null) newButtonItem = GameObject.Instantiate(buttonDeleteType);
                else newButtonItem = GameObject.Instantiate(buttonType);

                newButtonItem.GetComponent<Button>().onClick.AddListener(delegate() { OnButtonClick(item, row); });

                if (newButtonItem.GetComponentInChildren<Text>() != null)
                    newButtonItem.GetComponentInChildren<Text>().text = item.GetLabel();
                return newButtonItem;
            }
            else
            {
                return null;
            }
        }

        private GameObject CreateToggleItem(ListItem item, ListRow<T> row)
        {
            if (toggleType != null || toggleType.GetComponent<Button>() != null)
            {
                GameObject newToggleItem = GameObject.Instantiate(toggleType);
                newToggleItem.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool value) { OnToggleClick(value, item, row); });

                if (newToggleItem.GetComponentInChildren<Text>() != null)
                    newToggleItem.GetComponentInChildren<Text>().text = item.GetLabel();
                return newToggleItem;
            }
            else
            {
                return null;
            }
        }

        private void OnButtonClick(ListItem item, ListRow<T> row)
        {
            if (OnButtonPressed != null)
            {
                OnButtonPressed(item.GetType(), row);
            }
        }
        private void OnToggleClick(bool value, ListItem item, ListRow<T> row)
        {
            row.OnToggleClick(value);
            OnButtonClick(item, row);
        }

        public void SetToggleAll(bool select)
        {
            foreach (ListRow<T> item in items)
            {
                item.Selected = select;
            }
        }

        public bool Remove(ListRow<T> row)
        {
            if (row != null)
            {
                Debug.Log("ListTableView - Brisem zapis sa id " + row.Id);
                GameObject.DestroyImmediate(row.GameObject);
                items.Remove(row);
                return true;
            }
            return false;
        }

        public void RemoveAll()
        {
            Debug.Log("ListTableView - Brisem sve: Count " + items.Count);
            foreach (ListRow<T> item in items)
            {
                if(item.GameObject!=null) GameObject.DestroyImmediate(item.GameObject);
            }
            items.Clear();
            Debug.Log("ListTableView - Preostalo: Count " + items.Count);
        }

    }
}
