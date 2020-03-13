﻿using Dapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Domain.Base
{
    public abstract partial class BaseDapper : IDisposable
    {
        private ConnectionContainer _connectionContainer;
        private bool _leaveOpen;

        protected internal BaseDapper()
        {
            _connectionContainer = new ConnectionContainer()
            {
                Connection = BootAppConnection.CreateConnection()
            };
        }
        protected internal BaseDapper(ConnectionContainer container, bool leaveOpen)
        {
            _leaveOpen = leaveOpen;
            _connectionContainer = container;
        }

        public ConnectionContainer ConnectionContainer
        {
            get { return _connectionContainer; }
            protected set { _connectionContainer = value; }
        }

        protected internal bool LeaveOpen
        {
            get { return _leaveOpen; }
            set { _leaveOpen = value; }
        }

        public IDbTransaction BeginTransaction()
        {
            _connectionContainer.Transaction = _connectionContainer.Connection.BeginTransaction();
            return _connectionContainer.Transaction;
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            _connectionContainer.Transaction = _connectionContainer.Connection.BeginTransaction(isolationLevel);
            return _connectionContainer.Transaction;
        }

        /// <summary>
        /// Execute parameterized SQL  
        /// </summary>
        /// <returns>Number of rows affected</returns>
        public int Execute(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (transaction == null)
                transaction = _connectionContainer.Transaction;

            OpenConnection();
            var query = SqlMapper.Execute(_connectionContainer.Connection, sql, param, transaction, commandTimeout, commandType);
            return query;
        }

        /// <summary>
        /// Return a list of dynamic objects, reader is closed after the call
        /// </summary>
        public IEnumerable<dynamic> Query(string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (transaction == null)
                transaction = _connectionContainer.Transaction;

            OpenConnection();
            var query = SqlMapper.Query(_connectionContainer.Connection, sql, param, transaction, buffered, commandTimeout, commandType);
            return query;
        }

        /// <summary>
        /// Executes a query, returning the data typed as per T
        /// </summary>
        /// <remarks>the dynamic param may seem a bit odd, but this works around a major usability issue in vs, if it is Object vs completion gets annoying. Eg type new [space] get new object</remarks>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public IEnumerable<T> Query<T>(string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (transaction == null)
                transaction = _connectionContainer.Transaction;

            OpenConnection();
            var query = SqlMapper.Query<T>(_connectionContainer.Connection, sql, param, transaction, buffered, commandTimeout, commandType);
            return query;
        }

        /// <summary>
        /// Maps a query to objects
        /// </summary>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (transaction == null)
                transaction = _connectionContainer.Transaction;

            OpenConnection();
            var query = SqlMapper.Query<TFirst, TSecond, TReturn>(_connectionContainer.Connection, sql, map, param, transaction, buffered, splitOn,
                                                             commandTimeout, commandType);
            return query;
        }

        /// <summary>
        /// Perform a multi mapping query with 5 input parameters
        /// </summary>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (transaction == null)
                transaction = _connectionContainer.Transaction;

            OpenConnection();
            var query = SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(_connectionContainer.Connection, sql, map, param, transaction, buffered, splitOn,
                                                             commandTimeout, commandType);
            return query;
        }

        /// <summary>
        /// Perform a multi mapping query with 4 input parameters
        /// </summary>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (transaction == null)
                transaction = _connectionContainer.Transaction;

            OpenConnection();
            var query = SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TReturn>(_connectionContainer.Connection, sql, map, param, transaction, buffered, splitOn,
                                                             commandTimeout, commandType);
            return query;
        }

        /// <summary>
        /// Maps a query to objects
        /// </summary>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (transaction == null)
                transaction = _connectionContainer.Transaction;

            OpenConnection();
            var query = SqlMapper.Query<TFirst, TSecond, TThird, TReturn>(_connectionContainer.Connection, sql, map, param, transaction, buffered, splitOn,
                                                             commandTimeout, commandType);
            return AssignOwner(query);
        }

        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn
        /// </summary>
        public SqlMapper.GridReader QueryMultiple(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (transaction == null)
                transaction = _connectionContainer.Transaction;

            OpenConnection();

            return SqlMapper.QueryMultiple(_connectionContainer.Connection, sql, param, transaction, commandTimeout, commandType);
        }

        protected void OpenConnection()
        {
            if (_connectionContainer.Connection != null && _connectionContainer.Connection.State != ConnectionState.Open)
                _connectionContainer.Connection.Open();
        }

        protected void CloseConnection()
        {
            if (_connectionContainer.Connection != null)
                _connectionContainer.Connection.Close();
        }

        protected IEnumerable<T> AssignOwner<T>(IEnumerable<T> data)
        {
            if (!typeof(T).IsSubclassOf(typeof(BaseModel)))
            {
                return data;
            }
            return new DapperModelQuery<T>(data, _connectionContainer);
        }

        protected IList<T> AssignOwner<T>(IList<T> data)
        {
            if (!typeof(T).IsSubclassOf(typeof(BaseModel)))
            {
                return data;
            }
            for (int i = 0; i < data.Count; i++)
            {
                var baseModel = data[i] as BaseModel;
                if (baseModel != null)
                    baseModel.DbConnection = _connectionContainer;
            }
            return data;
        }
        protected void AssignListOwner<T>(IList<T> data) where T : BaseModel
        {
            for (int i = 0; i < data.Count; i++)
            {
                data[i].DbConnection = _connectionContainer;
            }
        }
        protected void AssignModelOwner<T>(T data) where T : BaseModel
        {
            if (data != null)
                data.DbConnection = _connectionContainer;
        }

        public void Dispose()
        {
            if (!_leaveOpen)
            {
                _connectionContainer.Dispose();
            }
            _connectionContainer = null;
        }
    }
}
