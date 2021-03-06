﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

using ComputersFactory.Data.SalesReports.Generator.XmlGenerators.Contracts;
using ComputersFactory.Models;

namespace ComputersFactory.Data.SalesReports.Generator.XmlGenerators
{
    public class SalesReportsXmlGenerator : IXmlGenerator<SalesReport>
    {
        private const string FileNameFormat = "{0}{1}{2}{3}";
        private const string RootDirectoryWithFileName = "../../../XmlSalesReports/SalesReports.xml";
        private const string RootDirectory = "../../../XmlSalesReports";
        private const string Separator = "/";
        private const string Extension = ".xml";

        public void GenererateXmlFiles(IEnumerable<SalesReport> salesReports)
        {
            if (salesReports == null)
            {
                throw new ArgumentNullException(nameof(salesReports));
            }

            var rootDirectoryInfo = new DirectoryInfo(SalesReportsXmlGenerator.RootDirectory);
            if (!rootDirectoryInfo.Exists)
            {
                Directory.CreateDirectory(rootDirectoryInfo.FullName);
            }

            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;

            using (var writer = XmlWriter.Create(SalesReportsXmlGenerator.RootDirectoryWithFileName, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Reports");

                foreach (var report in salesReports)
                {
                    this.GenerateReportXml(writer, report);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        private void GenerateReportXml(XmlWriter writer, SalesReport report)
        {
            writer.WriteStartElement("SalesReport");
            writer.WriteAttributeString("ComputerShopId", report.ComputerShopId.ToString());

            writer.WriteElementString("TotalAmount", report.TotalAmount.ToString());
            writer.WriteElementString("Date", report.Date.ToString("o"));

            writer.WriteStartElement("Sales");
            foreach (var sale in report.Sales)
            {
                this.GenerateSaleXml(writer, sale);
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        private void GenerateSaleXml(XmlWriter writer, Sale sale)
        {
            writer.WriteStartElement("Sale");
            writer.WriteAttributeString("ComputerId", sale.ComputerId.ToString());

            writer.WriteElementString("Amount", sale.Amount.ToString());

            writer.WriteEndElement();
        }

        private string ResolveFileName(DateTime timeOfReport)
        {
            var reportDate = timeOfReport.ToShortDateString();

            var fileName = string.Format(
                SalesReportsXmlGenerator.FileNameFormat,
                SalesReportsXmlGenerator.RootDirectory,
                SalesReportsXmlGenerator.Separator,
                reportDate,
                SalesReportsXmlGenerator.Extension);

            return fileName;
        }
    }
}
