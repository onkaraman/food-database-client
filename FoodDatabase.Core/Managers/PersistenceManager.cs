using System;
using System.Collections.Generic;
using ActionRadar.Core.Managers;
using FoodDatabase.Core.Patterns;
using FoodDatabase.Core.Persistence.Models;

namespace FoodDatabase.Core.Managers
{
    /// <summary>
    /// This class manages DB based persistence operations.
    /// </summary>
    public class PersistenceManager : LazyStatic<PersistenceManager>
    {
        private List<SimpleDBItem> _items;

        public PersistenceManager()
        {
            _items = DBManager.Static.DBAccessor.Select<SimpleDBItem>();
        }

        /// <summary>
        /// Adds a new item to the list by replacing the existing name
        /// if it does already exist. Otherwise it will be added as new.
        /// </summary>
        public void Add(string key, string value)
        {
            SimpleDBItem sdbi = new SimpleDBItem(key, value);

            for (int i = 0; i < _items.Count; i += 1)
            {
                if (_items[i].Key.Equals(key))
                {
                    _items[i] = sdbi;
                    break;
                }
            }
            _items.Add(sdbi);
        }

        /// <summary>
        /// Will add and instantly persist the added item to the DB.
        /// </summary>
        public void AddAndPersist(string key, string value)
        {
            Add(key, value);
            PersistAllToDB();
        }

        /// <summary>
        /// Will write all APIJsonResponses to the database.
        /// </summary>
        public void PersistAllToDB()
        {
            DBManager.Static.DBAccessor.Clear<SimpleDBItem>();
            DBManager.Static.DBAccessor.Insert(_items);
        }
                          
        /// <summary>
        /// Will extract a persisted SimpleDBItem response from the DB.
        /// </summary>
        public SimpleDBItem GetFirst(string key)
        {
            if (_items.Count == 0) throw new Exception("No items persisted.");
            foreach(SimpleDBItem sdbi in _items)
            {
                if (sdbi.Key.Equals(key)) return sdbi;
            }
            return null;
        }

        /// <summary>
        /// Will return a list of simple items where all keys are the same.
        /// </summary>
        public List<SimpleDBItem> GetList(string key)
        {
            if (_items.Count == 0) throw new Exception("No items persisted.");
            List<SimpleDBItem> list = new List<SimpleDBItem>();

            foreach (SimpleDBItem sdbi in _items)
            {
                if (sdbi.Key.StartsWith(key)) list.Add(sdbi);
            }
            return list;
        }
    
        /// <summary>
        /// Will clear all persisted data from the DB.
        /// </summary>
        public void DeleteAll()
        {
            DBManager.Static.DBAccessor.Clear<SimpleDBItem>();
        }

        /// <summary>
        /// Will delete a single db item from the db.
        /// </summary>
        public void Delete(SimpleDBItem sdbi)
        {
            DBManager.Static.DBAccessor.Delete(sdbi);
        }
    }
}

