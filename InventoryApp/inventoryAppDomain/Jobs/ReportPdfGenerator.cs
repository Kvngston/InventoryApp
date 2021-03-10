﻿using System.IO;
using inventoryAppDomain.Entities.Enums;
using inventoryAppDomain.Services;
using IronPdf;

namespace inventoryAppDomain.Jobs
{
    public class ReportPdfGenerator
    {
        public IReportService ReportService { get; }

        public ReportPdfGenerator(IReportService reportService)
        {
            ReportService = reportService;
        }
        
        
        public PdfDocument GenerateReportPdf(TimeFrame timeFrame)
        {
            var folderPath = Directory.GetCurrentDirectory();
            var report = ReportService.CreateReport(timeFrame);
            var renderer = new HtmlToPdf();
            var pdf = renderer.RenderHtmlAsPdf(report.DrugSales).SaveAs($"C:\\Users\\tochu\\Documents\\C# Projects\\inventoryapp\\InventoryApp\\report.pdf");
            pdf.AddHTMLHeaders(new HtmlHeaderFooter
            {
                CenterText = "PDF REPORT",
                FontSize = 24
            });
            return pdf;
        }
    }
}