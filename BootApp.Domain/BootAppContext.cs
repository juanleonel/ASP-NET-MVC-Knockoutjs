using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public partial class BootAppContext : IDisposable
    {
		private ConnectionContainer _connectionContainer;

		public ConnectionContainer DbConnection
		{
			get { return _connectionContainer; }
			set { _connectionContainer = value; }
		}

	 

		public void Dispose()
		{ 
		
		}

	}
}
