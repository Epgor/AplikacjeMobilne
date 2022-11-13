using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAppCross.Models;

namespace TestAppCross.Services
{
	public interface IDataService
	{
        Task<string> GetHealthCheck();
        Task<CinemaDTO> GetCinema();
	}
}