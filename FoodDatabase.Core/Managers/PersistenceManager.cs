﻿using System;
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
        public void AddSimpleDBItem(string key, string value)
        {
            SimpleDBItem sdbi = new SimpleDBItem(key, value);
            
            for (int i = 0; i < _items.Count; i+=1)
            {
                if (_items[i].Key.Equals(key))
                {
                    _items[i] = sdbi;
                    return;
                }
            }
            _items.Add(sdbi);
        }

        /// <summary>
        /// Will write all APIJsonResponses to the database.
        /// </summary>
        public void PersistAllToDB()
        {
            DBManager.Static.DBAccessor.Clear<SimpleDBItem>();
            DBManager.Static.DBAccessor.Insert<SimpleDBItem>(_items);
        }

        /// <summary>
        /// Will clear and write the passed model to the DB.
        /// </summary>
        public void PersistAllToDB<T>(T model) where T : new()
        {
            DBManager.Static.DBAccessor.Clear<T>();
            DBManager.Static.DBAccessor.Insert<T>(model);
        }

        /// <summary>
        /// Will return the first object of the requested table.
        /// </summary>
        public T GetFirst<T>() where T : new()
        {
            if (!DBManager.Static.DBAccessor.IsEmpty<T>())
            {
                return DBManager.Static.DBAccessor.Select<T>()[0];
            }
            throw new NullReferenceException();
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
        /// Will clear all persisted data from the DB.
        /// </summary>
        public void ClearAll()
        {
            DBManager.Static.DBAccessor.Clear<SimpleDBItem>();
        }
    }
}

