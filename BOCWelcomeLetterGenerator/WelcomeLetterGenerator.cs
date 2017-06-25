using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfFileWriter;
using System.Drawing;


namespace BOCWelcomeLetterGenerator {
    static class Pdf_Generation_Class {
        private static PdfFont ArialNormal;
        private static PdfFont ArialBold;
        private static PdfFont ArialItalic;
        private static PdfFont ArialBoldItalic;
        private static PdfFont TimesNormal;
        private static PdfFont SimHei;
        private static PdfFont Comic;
        private static PdfDocument document;
        private static PdfPage Page;
        private static PdfContents Contents;

        private enum Month {
            JAN, FEB, MAR, APR, MAY, JUN,
            JUL, AUG, SEP, OCT, NOV, DEC
        }

        ////////////////////////////////////////////////////////////////////
        // Create article's example test PDF document
        ////////////////////////////////////////////////////////////////////

        public static void GenerateWelcomeLetterSimple(String FileName, CustomerInformation customer, List<Account> accountList, BranchInfo branch, bool inclEmail, Boolean Debug = false) {
            // Create an empty pdf document in A4 and measured in cm
            bool landscape = false;
            document = new PdfDocument(PaperType.A4, landscape, UnitOfMeasure.cm, FileName);
            
            // Set debug tag
            document.Debug = Debug;

            // Write Pdf info
            PdfInfo Info = PdfInfo.CreatePdfInfo(document);
            Info.Title("Welcome Letter - Bank of China");
            Info.Author("Bank of China "+ branch.GetAddress()[0]);
            Info.Keywords("Account, Bank of China, Remittance");
            Info.Subject("Remittance information for Bank of China accounts");

            // define font resources
            DefineFontResources();

            // Add first page, also the only page for simple welcome letter
            Page = new PdfPage(document);

            // Add contents to page
            Contents = new PdfContents(Page);

            // Add graphices and text contents to the contents object
            DrawTitle();
            if (customer.HasAddress()) {
                DrawCustomerNameAddress(customer);
            }
            DrawBranchNameAddress(branch,inclEmail);
            DrawLetterGreeting(customer);
            DrawBankInformation(customer);
            DrawAccountInformationForm(accountList);
            DrawLetterBody();
            DrawGroupContact();

            // Create pdf file
            document.CreateFile();

            // exit
            return;
        }

        ////////////////////////////////////////////////////////////////////
        // Define Font Resources
        ////////////////////////////////////////////////////////////////////

        private static void DefineFontResources() {
            // Define font resources
            // Arguments: PdfDocument class, font family name, font style, embed flag
            // Font style (must be: Regular, Bold, Italic or Bold | Italic) All other styles are invalid.
            // Embed font. If true, the font file will be embedded in the PDF file.
            // If false, the font will not be embedded
            String arialFontName = "Arial";
            String timesNewRomanFontName = "Times New Roman";

            ArialNormal = PdfFont.CreatePdfFont(document, arialFontName, System.Drawing.FontStyle.Regular, true);
            ArialBold = PdfFont.CreatePdfFont(document, arialFontName, System.Drawing.FontStyle.Bold, true);
            ArialItalic = PdfFont.CreatePdfFont(document, arialFontName, System.Drawing.FontStyle.Italic, true);
            ArialBoldItalic = PdfFont.CreatePdfFont(document, arialFontName, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, true);
            TimesNormal = PdfFont.CreatePdfFont(document, timesNewRomanFontName, System.Drawing.FontStyle.Regular, true);
            Comic = PdfFont.CreatePdfFont(document, "Comic Sans MS", System.Drawing.FontStyle.Bold, true);
            SimHei = PdfFont.CreatePdfFont(document, "SimHei", System.Drawing.FontStyle.Regular, true);
            return;
        }

        ////////////////////////////////////////////////////////////////////
        // Draw image and clip it
        ////////////////////////////////////////////////////////////////////

        private static void DrawTitle() {
            // define local image resources as 200ppi and 100% quality
            PdfImageControl ImageControl = new PdfImageControl();
            ImageControl.Resolution = 200;
            ImageControl.ImageQuality = 100;
            // Get image from embedded local resource
            Image BocTitle = Properties.Resources.BocTitleBitmap;
            PdfImage titleImage = new PdfImage(document, BocTitle, ImageControl);

            // save graphics state
            Contents.SaveGraphicsState();

            // set coordinate
            Contents.Translate(1.3, 26.8);

            // set image size
            PdfRectangle size = titleImage.ImageSizePosition(18.39, 1.85, ContentAlignment.MiddleCenter);

            // clipping path
            Contents.DrawRectangle(size.Left, size.Bottom, size.Width, size.Height, PaintOp.ClipPathEor);

            // draw image
            Contents.DrawImage(titleImage, size.Left, size.Bottom, size.Width, size.Height);

            // restore graphics state
            Contents.RestoreGraphicsState();
            return;
        }

        ////////////////////////////////////////////////////////////////////
        // Draw example of a text box
        ////////////////////////////////////////////////////////////////////

        private static void DrawCustomerNameAddress(CustomerInformation customer) {
            // save graphics state
            Contents.SaveGraphicsState();

            // set coordinate
            Contents.Translate(1.3, 16.2);

            // Define constants
            const Double Width = 18;
            const Double Height = 8;
            const Double FontSize = 10;

            // Create text box object with no first line indent
            TextBox customerContact = new TextBox(Width, 0);

            // add text to the text box
            customerContact.AddText(ArialNormal, FontSize, customer.GetName() + "\n");
            customerContact.AddText(ArialNormal, FontSize, customer.GetAddress()[0] + "\n");
            customerContact.AddText(ArialNormal, FontSize, customer.GetAddress()[1] + "\n");
            customerContact.AddText(ArialNormal, FontSize, customer.GetAddress()[2] + "\n");

            // Draw the text box
            Double PosY = Height;
            Contents.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.Left, customerContact);

            // restore graphics state
            Contents.RestoreGraphicsState();
            return;
        }

        private static void DrawBranchNameAddress(BranchInfo branch, bool inclEmail) {
            // save graphics state
            Contents.SaveGraphicsState();

            // set coordinate
            Contents.Translate(10, 18.7);
            
            // Define constants
            const Double Width = 9.65;
            const Double Height = 8;
            const Double FontSize = 10;

            // Create text box object with no first line indent
            TextBox branchContact = new TextBox(Width, 0);

            // print address
            List<string> branchAddress = branch.GetContact(inclEmail);
            branchAddress[0] = "Bank of China " + branchAddress[0]; 
            foreach (string line in branchAddress) {
                branchContact.AddText(ArialNormal, FontSize, line + "\n");
            }
            // print web address
            branchContact.AddText(ArialNormal, FontSize, "http://www.bankofchina.com/au/");

            // Draw the text box
            Double PosY = Height;
            Contents.DrawText(0.0, ref PosY, 0.0, 0, 0, 0, TextBoxJustify.Right, branchContact);


            // restore graphics state
            Contents.RestoreGraphicsState();
            return;
        }

        private static void DrawLetterGreeting(CustomerInformation customer) {
            // Save graphics state
            Contents.SaveGraphicsState();

            // Set coordinate
            Contents.Translate(1.3, 14);

            // Define constants
            const Double Width = 18;
            const Double Height = 8;
            const Double FontSize = 10;

            // Create text box object with no first line indent
            TextBox headMessage = new TextBox(Width, 0);

            // add text to the text box
            headMessage.AddText(ArialNormal, FontSize, "Dear " + customer.GetName() + ",\n\n");
            headMessage.AddText(ArialNormal, FontSize, Properties.Settings.Default.SimpleLetterGreatingEng + Environment.NewLine);
            headMessage.AddText(SimHei, FontSize, Properties.Settings.Default.SimpleLetterGreatingChn + Environment.NewLine);


            // Draw the text box
            Double PosY = Height;
            Contents.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.Left, headMessage);

            // restore graphics state
            Contents.RestoreGraphicsState();
            return;
        }

        private static void DrawLetterBody() {
            // save graphics state
            Contents.SaveGraphicsState();

            // translate origin to PosX=1.1" and PosY=1.1" this is the bottom left corner of the text box example
            Contents.Translate(1.3, 5.3);

            // Define constants
            const Double Width = 18.39;
            const Double Height = 8;
            const Double FontSize = 10;

            // Create text box
            TextBox Box = new TextBox(Width, 0);

            // get date
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string monthAbriviation = ((Month)(int.Parse(mm)-1)).ToString();
            string yyyy = DateTime.Now.Year.ToString();

            // add text to the text box
            Box.AddText(ArialNormal, FontSize, Properties.Settings.Default.SimpleLetterBodyLine1Eng + Environment.NewLine);
            Box.AddText(SimHei, FontSize, Properties.Settings.Default.SimpleLetterBodyLine1Chn + Environment.NewLine + Environment.NewLine);
            Box.AddText(ArialNormal, FontSize, Properties.Settings.Default.SimpleLetterBodyLine2Eng + Environment.NewLine);
            Box.AddText(SimHei, FontSize, Properties.Settings.Default.SimpleLetterBodyLine2Chn + Environment.NewLine + Environment.NewLine);
            Box.AddText(ArialNormal, FontSize, "Should you have any enquiry please do not hesitate to contact us.\n");
            Box.AddText(SimHei, FontSize, "如有任何疑问，请不吝联系我们。\n\n");
            Box.AddText(ArialNormal, FontSize, "Regards,\n");
            Box.AddText(SimHei, FontSize, "谢谢。\n\n");
            Box.AddText(ArialNormal, FontSize, "Bank of China (Australia) Ltd\n");
            Box.AddText(SimHei, FontSize, "中国银行（澳大利亚）\n\n");
            Box.AddText(ArialNormal, FontSize, dd + " " + monthAbriviation + " " + yyyy + "\n");
            Box.AddText(SimHei, FontSize, yyyy + "年" + mm + "月" + dd + "日");

            // Draw the text box
            Double PosY = Height;
            Contents.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.Left, Box);

            // restore graphics state
            Contents.RestoreGraphicsState();
            return;
        }

        ////////////////////////////////////////////////////////////////////
        // Draw example of order form
        ////////////////////////////////////////////////////////////////////

        private static void DrawBankInformation(CustomerInformation customer) {
            // Define constants to make the code readable
            const Double LEFT = 6.3;
            const Double TOP = 19.5;
            const Double BOTTOM = 17.3;
            const Double RIGHT = 6.3 + 15.39;
            const Double FONT_SIZE = 10;
            const Double MARGIN_HOR = 0.04;
            const Double MARGIN_VER = 0.04;

            // preset content
            string[,] content = new string[,] {
                {"BSB Number:  ","980200" },
                {"Swift Code:  ","BKCHAU2AXXX"},
                {"Bank Name:  ", "Bank of China (Australia) Ltd" }
            };


            // column widths
            Double colWidthTitle = ArialNormal.TextWidth(FONT_SIZE, "Account Name:  ") + 2.0 * MARGIN_HOR;
            Double colWidthDetail = ArialNormal.TextWidth(FONT_SIZE, "A very very very long name example and may be longer") + 2.0 * MARGIN_HOR;

            // define table
            PdfTable Table = new PdfTable(Page, Contents, ArialNormal, FONT_SIZE);
            Table.TableArea = new PdfRectangle(LEFT, BOTTOM, RIGHT, TOP);
            Table.SetColumnWidth(new Double[] { colWidthTitle, colWidthDetail });

            // define borders
            Table.Borders.ClearAllBorders();

            // margin
            PdfRectangle Margin = new PdfRectangle(MARGIN_HOR, MARGIN_VER);

            // default header style
            Table.DefaultHeaderStyle.Margin = Margin;
            Table.DefaultHeaderStyle.BackgroundColor = Color.White;
            Table.DefaultHeaderStyle.Alignment = ContentAlignment.MiddleLeft;

            // table heading
            Table.Header[0].Value = "Account Name:  ";
            Table.Header[1].Value = customer.GetName().ToUpper();

            // account type style
            Table.DefaultCellStyle.Margin = Margin;

            // loop for all items
            for (int i = 0; i < content.GetLength(0); i++) {
                for (int j = 0; j < content.GetLength(1); j++) {
                    Table.Cell[j].Value = content[i, j];
                }
                Table.DrawRow();
            }
            Table.Close();

            // save graphics state
            Contents.SaveGraphicsState();

            // restore graphics state
            Contents.RestoreGraphicsState();
            return;
        }

        private static void DrawAccountInformationForm(List<Account> accountList) {
            // Define constants to make the code readable
            const Double LEFT = 1.3;
            const Double TOP = 16.5;
            const Double BOTTOM = 10.3;
            const Double RIGHT = 1.3 + 18.39;
            const Double FONT_SIZE = 10;
            const Double MARGIN_HOR = 0.04;
            const Double MARGIN_VER = 0.04;
            const Double FRAME_WIDTH = 0.015;

            // column widths
            Double colWidthType = ArialNormal.TextWidth(FONT_SIZE, "Overseas Student Account") + 2.0 * MARGIN_HOR;
            Double colWidthCcy = ArialNormal.TextWidth(FONT_SIZE, "AUD") + 2.0 * MARGIN_HOR;
            Double colWidthLongNumber = ArialNormal.TextWidth(FONT_SIZE, "  International Transfer  ") + 2.0 * MARGIN_HOR;
            Double colWidthShortNumber = ArialNormal.TextWidth(FONT_SIZE, "  Domestic Transfer  ") + 2.0 * MARGIN_HOR;


            // define table
            PdfTable Table = new PdfTable(Page, Contents, ArialNormal, FONT_SIZE);
            Table.TableArea = new PdfRectangle(LEFT, BOTTOM, RIGHT, TOP);
            Table.SetColumnWidth(new Double[] { colWidthType, colWidthCcy, colWidthLongNumber, colWidthShortNumber });

            // define borders
            Table.Borders.ClearAllBorders();
            Table.Borders.SetCellHorBorder(FRAME_WIDTH);
            Table.Borders.SetHeaderHorBorder(FRAME_WIDTH);
            Table.Borders.SetTopBorder(FRAME_WIDTH);
            Table.Borders.SetBottomBorder(FRAME_WIDTH);


            // margin
            PdfRectangle Margin = new PdfRectangle(MARGIN_HOR, MARGIN_VER);

            // default header style
            Table.DefaultHeaderStyle.Margin = Margin;
            Table.DefaultHeaderStyle.BackgroundColor = Color.White;
            Table.DefaultHeaderStyle.Alignment = ContentAlignment.MiddleCenter;
            Table.DefaultHeaderStyle.Font = ArialBold;

            // table heading
            Table.Header[0].Value = "Account Type";
            Table.Header[1].Value = "CCY";
            Table.Header[2].Value = "International Transfer";
            Table.Header[3].Value = "Domestic Transfer";

            // account type style
            Table.DefaultCellStyle.Margin = Margin;

            // description column style
            for (int i = 0; i < 4; i++) {
                Table.Cell[i].Style = Table.CellStyle;
                Table.Cell[i].Style.MultiLineText = false;
                Table.Cell[i].Style.Alignment = ContentAlignment.MiddleCenter;
            }

            // loop for all items
            foreach (Account account in accountList) {
                List<string> accountInfo = account.GetAccountInfo();
                for (int i = 0; i < 4; i++) {
                    Table.Cell[i].Value = accountInfo[i];
                }
                Table.DrawRow();
            }


            Table.Close();

            // save graphics state
            Contents.SaveGraphicsState();

            // restore graphics state
            Contents.RestoreGraphicsState();
            return;
        }


        private static void DrawGroupContact() {
            // Define constants
            const Double LEFT = 1.3;
            const Double TOP = 4.5;
            const Double BOTTOM = 1;
            const Double RIGHT = 1.3 + 18.39;
            const Double FONT_SIZE = 8;
            const Double MARGIN_HOR = 0.04;
            const Double MARGIN_VER = 0.04;

            BranchInfo branch = new BranchInfo(Branch.Banking);
            string[,] branchContact = branch.GetFullDataSet();
            int numOfBranches = branchContact.GetLength(0);
            int numOfColumns = (numOfBranches + 1) / 2;

            // column widths
            Double colWidth = 18.39 / numOfColumns;


            // define table
            PdfTable Table = new PdfTable(Page, Contents, ArialNormal, FONT_SIZE);
            Table.TableArea = new PdfRectangle(LEFT, BOTTOM, RIGHT, TOP);
            Table.SetColumnWidth(Enumerable.Repeat(colWidth, numOfColumns).ToArray());

            // define borders
            Table.Borders.ClearAllBorders();

            // margin
            PdfRectangle Margin = new PdfRectangle(MARGIN_HOR, MARGIN_VER);

            // account type style
            Table.DefaultCellStyle.Margin = Margin;

            for (int i = 0; i < 2*4; i++) {
                int index = i % 4;
                int startBranchIndex;
                if (i < 4) {
                    startBranchIndex = 0;
                } else {
                    startBranchIndex = numOfColumns;
                }
                int cell = 0;
                for (int j = startBranchIndex; j < startBranchIndex + numOfColumns; j++) {
                    Table.Cell[cell].Value = branchContact[j, index];
                    cell++;
                }
                Table.DrawRow();
                if (i == 3) {
                    Table.DrawRow();    //Draw 2 extra rows as spacer
                    Table.DrawRow();
                }
            }

            Table.Close();

            // save graphics state
            Contents.SaveGraphicsState();

            // restore graphics state
            Contents.RestoreGraphicsState();
            return;
        }
    }







}

