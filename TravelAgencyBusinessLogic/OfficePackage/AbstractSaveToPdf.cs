using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyBusinessLogic.OfficePackage.HelperEnums;
using TravelAgencyBusinessLogic.OfficePackage.HelperModels;

namespace TravelAgencyBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateAfter.ToShortDateString() } по { info.DateBefore.ToShortDateString() }", Style = "Normal"
            });
            CreateTable(new List<string> { "2cm", "2cm", "5cm", "3cm", "3cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата путешествия", "Название путешествия", "Имя гида", "Название места", "Количество мест" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var trip in info.Trips)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { trip.DateCreate.ToShortDateString(), trip.Name.ToString(), "", ""},
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
                for (int i = 0; i < Math.Max(trip.Guides.Count, trip.Places.Count); ++i)
                {
                    PdfRowParameters newItem = new PdfRowParameters();
                    newItem.Style = "Normal";
                    newItem.ParagraphAlignment = PdfParagraphAlignmentType.Center;
                    newItem.Texts = new List<string> { "", "", ""};
                    if (i < trip.Guides.Count)
                    {
                        newItem.Texts.Add(trip.Guides[i].GuideName);
                    }
                    else
                    {
                        newItem.Texts.Add("");
                    }
                    if (i < trip.Places.Count)
                    {
                        newItem.Texts.Add(trip.Places[i].Name);
                        newItem.Texts.Add(trip.Places[i].DateOfVisit.ToString());
                    }
                    else
                    {
                        newItem.Texts.Add("");
                        newItem.Texts.Add("");
                    }
                    CreateRow(newItem);
                }
            }
            SavePdf(info);
        }
        protected abstract void CreatePdf(PdfInfo info);
        protected abstract void CreateParagraph(PdfParagraph paragraph);
        protected abstract void CreateTable(List<string> columns);
        protected abstract void CreateRow(PdfRowParameters rowParameters);
        protected abstract void SavePdf(PdfInfo info);
    }
}
