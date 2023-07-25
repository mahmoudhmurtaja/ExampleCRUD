using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCRUDwhitAjax.Services
{
	public class PagedResultDto<T>
	{
		public T Data { get; set; }
		public int TotalCount { get; set; }
	}
}
