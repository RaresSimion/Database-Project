﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenModel;
using SomerenDAL;

namespace SomerenLogic
{
    public class RevenueReportService
    {
        RevenueReportDAO revenueReportDAO;

        public RevenueReportService()
        {
            revenueReportDAO = new RevenueReportDAO();
        }

        public RevenueReport GetReport(DateTime startDate, DateTime endDate)
        {
            return revenueReportDAO.GetReport(startDate, endDate);
        }
    }
}
