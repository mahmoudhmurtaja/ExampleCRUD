using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCRUDwhitAjax.Services
{
	public class PagedResultRequestDto<T> : IEnumerable
	{
		public T SearchValue { get; set; }
		public string SortColumn { get; set; }
		public string SortColumnDirection { get; set; }
		public int Skip { get; set; }
		public int PageSize { get; set; }

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
