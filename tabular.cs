
// This program created by Yi Song 2021/9/25
// It will convert between CSV (Comma Sepa- rated Values), MD (Markdown), JSON (JavaScript Object NotaCon, and HTML-TABLE (HTML <table> element) formats. 
// Comment the code update by Yi Song 2021/10/5
// add the .txt and .tex convert method update by Yi Song 2021/10/6

using System;
using System.Data;
using System.Text;
using System.Linq;
using System.IO;
using System.Net;

// set a array 1D that read from file, and get the array 2D row and column number
public class arrayColRowNumber
{
    // this is a array variable method get and set
    public string[] array1D { get; set; }
    // this is column number return
    public int colNumber { get {return array1D[0].Split(',').Length;}  }
    // this is row number return
    public int rowNumber { get {return array1D.Length;}  }
}

public class tabular
{      
        //mainUnicode.UniCode = "UTF8";
        public static string[,] CsvFileReadToArray(string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: reading Csv file to Array ..."); 
            }
            // new a object to get the array property value
            var lines = new arrayColRowNumber();
            // read the file to object array
            lines.array1D = System.IO.File.ReadAllLines(fName);
            //string[] lines = System.IO.File.ReadAllLines(fName);
            //string[] lines = System.IO.File.ReadAllLines(@"test.csv");
            // get the row numbers
            //int rowNum = lines.Length;
            // get the column numbers
            //int columnNum = lines[0].Split(',').Length;
            // set a array 2D table by row and column number
            //string[,] arrayTable = new string[rowNum, columnNum];
            //Console.WriteLine(lines.rowNumber);
            //Console.WriteLine(lines.colNumber);
            string[,] arrayTable = new string[lines.rowNumber, lines.colNumber];
            
            // init x variable zero for row loop
            int x = 0;       
            // loop the lines array every row to line
            foreach (string line in lines.array1D)
            {
               // split the line to a array by ,
               var data=line.Split(',');
               // loop the every row array value 
               for (int y = 0; y <= lines.colNumber-1; y++)
                {
                    //save the row array data value to array 2D table
                    arrayTable[x,y]=data[y].Trim('"');                  
                }
                // set x value add 1
                x = x + 1;    
            }
            // return the array table
            return arrayTable;
        }

        public static string[,] MdFileReadToArray(string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: reading Md file to Array ..."); 
            }
            // read the file to array
            string[] lines = System.IO.File.ReadAllLines(fName);
            //string[] lines = System.IO.File.ReadAllLines(@"test.md");
            // get the row numbers, remove ------ line -1
            int rowNum = lines.Length - 1;
            // get the column numbers, remove empty line 
            int columnNum = lines[0].Split('|',StringSplitOptions.RemoveEmptyEntries).Length;          
            // set a array 2D table by row and column number
            string[,] arrayTable = new string[rowNum, columnNum];
            
            // init x variable zero for row loop
            int x = 0; 
            // init x variable zero for line loop
            int lineNum = 0;      
            // loop the lines array every row to line
            foreach (string line in lines)
            {
               // skip the second ------ line
               if (lineNum!=1)
               {
               // split the line to a array by , remove empty line 
               var data=line.Split('|',StringSplitOptions.RemoveEmptyEntries);
               // loop the every value of row array  
               for (int y = 0; y <= columnNum-1; y++)
                {
                    //save the row array data value to array 2D table
                    arrayTable[x,y]=data[y];
                }
                // set x value add 1
                x = x + 1;           
               }  
               // set line number value add 1
               lineNum = lineNum + 1;              
            }
            // return the array table
            return arrayTable;          
        } 

        public static string[,] JsonFileReadToArray(string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: reading Json file to Array ..."); 
            }
            // read the file to array
            string[] lines = System.IO.File.ReadAllLines(fName);
            //string[] lines = System.IO.File.ReadAllLines(@"test.json");
            //string JsonText = System.IO.File.ReadAllText(@"test.json");

            // merge array to string text, because we don't how many line json file have.
            string jsonText= string.Join("", lines);
            // Split string text to array by '{','}',',' char 
            string[] jsonTextSplit = jsonText.Split('{','}',',');

            // count the { number to get the row data number 
            int rowdataNum = jsonText.Split('{').Length - 1 ;
            // plus the column row 1 line equals all row number
            int rowNum = rowdataNum + 1;
            // Count the : number to get the column number that include every row in json, so need / row number
            int columnNum = (jsonText.Split(':').Length - 1) / rowdataNum;
            // init a 2D array table 
            string[,] arrayTable = new string[rowNum, columnNum];
            // set a variable for after loop column count number begin
            int cntColumn = 1;
            // set a bool paramter this is first column
            bool firstColumn = true;
            // set x, y for array
            int x = 0;
            int y = 0;
             
            // loop each line of json text array
            foreach (string line in jsonTextSplit)
            {
                // if contain : so that is a data row
                if (line.Contains(":"))
                {
                   // split line to array by :
                   var data=line.Split(':');  
                   // check if is first column, and it will handle column line and the first data line
                   if (firstColumn)
                   {                          
                        //save the row array data value to array 2D table
                        // this is column line
                        arrayTable[x,y]=data[0].Trim().Trim('"');
                        // this is data line
                        arrayTable[x+1,y]=data[1].Trim().Trim('"'); 
                    }
                    else
                    {
                        // this is data line
                        arrayTable[x,y]=data[1].Trim().Trim('"');
                    } 
                    // if count column number equals column number
                    if (cntColumn == columnNum)
                    {
                        // recount from 1
                        cntColumn = 1 ;
                        // if is first column
                        if (firstColumn)
                        {
                            // the next handle from the 3 line, becuase the 2 line already handle in firstColumn if condition
                            x = 2;
                        }
                        else
                        {
                            // go next line
                            x = x + 1;
                        }
                        // tell the first column handle is done
                        firstColumn = false;
                        // reset y value from -1, because after have y=y+1 to go 0 begin
                        y = -1;
                    }
                    else
                    {
                        // repeat count column number
                        cntColumn = cntColumn + 1; 
                    }
                    // set y value add 1
                    y = y + 1;         
                }             
            }
            // return array 2D table to main 
            return arrayTable;
        }

        public static string[,] HtmlFileReadToArray(string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: reading Html file to Array ..."); 
            }
            // read the file to array
            string[] lines = System.IO.File.ReadAllLines(fName);
            //string[] lines = System.IO.File.ReadAllLines(@"test.html");
            // merge array to string text, because we don't how many line html file have.
            string htmlText= string.Join("", lines);
            // Split string text to array by "</th>","</td>","</table>","</tr>"  char 
            // https://stackoverflow.com/questions/1254577/string-split-by-multiple-character-delimiter
            string[] htmlTextSplit = htmlText.Split(new string[] { "</th>","</td>","</table>","</tr>"  }, StringSplitOptions.None);

            // count the "</th>" number  -1 to get the column number by using split
            int columnNum = htmlText.Split(new string[] { "</th>" }, StringSplitOptions.None).Length - 1 ;

            // count the "</td>" number  -1 to get the row data number by using split in the html, so need / columnNum number
            int rowdataNum =   (htmlText.Split(new string[] { "</td>" }, StringSplitOptions.None).Length - 1) / columnNum ;
            
            /*
            foreach (string line in htmlTextSplit)
            {
              var xxx = line.Substring(line.IndexOf("<th"),line.Length-line.IndexOf("<th"));
              Console.WriteLine(xxx);
              //Console.WriteLine(line);
            }
            */
            
            //  plus the column row 1 line equals all row number
            int rowNum = rowdataNum + 1;
            // init a 2D array table 
            string[,] arrayTable = new string[rowNum, columnNum];
            // set a variable for after loop column count number begin
            int cntColumn = 1;
            // set a bool paramter this is first column
            bool firstColumn = true;
            // set x, y for array
            int x = 0;
            int y = 0;

            // loop each line of html html array
            foreach (string line in htmlTextSplit)
            {
                // if contain <th so that is a column line
                if (line.Contains("<th") | line.Contains("<td"))
                {
                   // if this is the first column
                   if (firstColumn)
                   {        
                       // set a paramter to substr the line from <th begin to the end        
                       var tagAfter = line.Substring(line.IndexOf("<th"),line.Length-line.IndexOf("<th")); 
                       // set a paramter to substr the line from > begin to the end, so that get the column name
                       var getTagValue = tagAfter.Substring(tagAfter.IndexOf(">")+1,tagAfter.Length-tagAfter.IndexOf(">")-1);           
                        //save the column row array data value to array 2D table
                        arrayTable[x,y]=getTagValue;
                    }
                    else
                    {
                        // set a paramter to substr the line from <td begin to the end 
                        var tagAfter = line.Substring(line.IndexOf("<td"),line.Length-line.IndexOf("<td")); 
                        // set a paramter to substr the line from > begin to the end, so that get the data value
                        var getTagValue = tagAfter.Substring(tagAfter.IndexOf(">")+1,tagAfter.Length-tagAfter.IndexOf(">")-1); 
                        //save the data row array data value to array 2D table
                        arrayTable[x,y]=getTagValue;
                    } 
                    // if count column number equals column number
                    if (cntColumn == columnNum)
                    {
                        // set the value from begin 1
                        cntColumn = 1 ;
                        // set x value add 1
                        x = x + 1;
                        // tell the first column handle is done
                        firstColumn = false;
                        // reset y value from -1, because after have y=y+1 to go 0 begin
                        y = -1;
                    }
                    else
                    {
                        // repeat count column number
                        cntColumn = cntColumn + 1; 
                    }
                    // set y value add 1
                    y = y + 1;         
                }             
            }
            // return array 2D table to main 
            return arrayTable;    
        }       

        public static string[,] TxtFileReadToArray(string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: reading Txt file to Array ..."); 
            }
            // read the file to array
            string[] lines = System.IO.File.ReadAllLines(fName);
            //string[] lines = System.IO.File.ReadAllLines(@"test.txt");
            // get the row numbers, remove ------ line -1
            int rowNum = (lines.Length - 1) / 2;
            // get the column numbers, remove empty line 
            int columnNum = lines[0].Split('+',StringSplitOptions.RemoveEmptyEntries).Length;          
            // set a array 2D table by row and column number
            string[,] arrayTable = new string[rowNum, columnNum];
            
            // init x variable zero for row loop
            int x = 0; 
            // init x variable zero for line loop
            int lineNum = 0;  
            // set a variable if is Even
            int numSeq = 1;    
            // loop the lines array every row to line
            foreach (string line in lines)
            {
               // if line number is even
               if (numSeq % 2==0)
               {
               // split the line to a array by , remove empty line 
               var data=line.Split('|',StringSplitOptions.RemoveEmptyEntries);
               // loop the every value of row array  
               for (int y = 0; y <= columnNum-1; y++)
                    {
                        //save the row array data value to array 2D table
                        arrayTable[x,y]=data[y];
                    }
                // set x value add 1
                x = x + 1;           
               }  
               // set line number value add 1
               lineNum = lineNum + 1;  
               // seq add 1
               numSeq  = numSeq + 1;          
            }
            // return the array table
            return arrayTable;          
        } 

        public static string[,] TexFileReadToArray(string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: reading Tex file to Array ..."); 
            }
            // read the file to array
            string[] lines = System.IO.File.ReadAllLines(fName);
            //string[] lines = System.IO.File.ReadAllLines(@"test.tex");
            // get the row numbers, remove ------ line -1
            int rowNum = lines.Length - 4;
            // get the column numbers, remove empty line 
            int columnNum = lines[2].Split('&',StringSplitOptions.RemoveEmptyEntries).Length;          
            // set a array 2D table by row and column number
            string[,] arrayTable = new string[rowNum, columnNum];
            
            // init x variable zero for row loop
            int x = 0; 
            // init x variable zero for line loop
            int lineNum = 0;      
            // loop the lines array every row to line
            foreach (string line in lines)
            {
               // not include begin and end tag
               if (! (line.Contains("\\begin") | line.Contains("\\end"))) 
               {
               // split the line to a array by , remove empty line 
               var data=line.Trim('\\').Split('&',StringSplitOptions.RemoveEmptyEntries);
               // loop the every value of row array  
               for (int y = 0; y <= columnNum-1; y++)
                {
                    //save the row array data value to array 2D table
                    arrayTable[x,y]=data[y].Trim();
                }
                // set x value add 1
                x = x + 1;           
               }  
               // set line number value add 1
               lineNum = lineNum + 1;              
            }
            // return the array table
            return arrayTable;          
        } 

        // follow is array to file
        public static void arrayToCsv(string[,] args,string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: writing Array to Csv file ..."); 
            }
            // set a document content variable
            string docCsv = "";
            //for loop array 2D reference https://stackoverflow.com/questions/8184306/iterate-through-2-dimensional-array-c-sharp                    
            for (int k = 0; k < args.GetLength(0); k++)
            {
                // set a line paramter
                string toLine = "";
                // loop each value of the row line 
                for (int l = 0; l < args.GetLength(1); l++)
                {
                    // set a the value variable 
                    string getTheValue;
                    // check if is a number
                    if (decimal.TryParse(args[k, l], out decimal n))
                    {
                      // if is a number
                      getTheValue= args[k, l].Trim();                     
                    }
                    else
                    {
                      // if is not a number
                      getTheValue= "\"" + args[k, l].Trim() + "\"" ;
                    }
                    if (l==0) 
                    {
                      // if is a first value
                      toLine = toLine + getTheValue; 
                    }
                    else
                    {
                      // if is not a first value, should add , in front 
                      toLine = toLine + "," + getTheValue;
                    }                       
                }
                // if is a first line             
                if (k==0)
                {
                   // document content is the line
                   docCsv = toLine ; 
                }
                else
                {
                   // if is not the first line 
                   docCsv = docCsv + Environment.NewLine + toLine ; 
                }
          
            }
            //Console.WriteLine(docCsv);
            //var fileName = fName;
            //var fileName = @"output.csv";
            // write text to file 
            File.WriteAllText(fName,docCsv);   //Encoding.UTF8   
            // show the message
            Console.WriteLine("Wrote to file: {0}", fName);
            Console.WriteLine();
        }


        public static void arrayToMd(string[,] args,string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: writing Array to Md file ..."); 
            }
            // set a document content variable
            string docMd = "";
            // set a array for get the max char length of the column
            int[] maxLength = new int[args.GetLength(1)];
            //for loop array 2D reference https://stackoverflow.com/questions/8184306/iterate-through-2-dimensional-array-c-sharp                              
            for (int k = 0; k < args.GetLength(0); k++)
            {
                // loop each value of the row 
                for (int l = 0; l < args.GetLength(1); l++)
                {
                   // if the value > max length of this column
                   if (args[k, l].Length>maxLength[l])
                   {
                       // set the value to the max length of this column
                       maxLength[l]=args[k, l].Length;
                   }
                }
            }
            // set the no value line begin with "|"
            String blankLine = "|";
            // loop every column
            for (int k = 0; k < args.GetLength(0); k++)
            {
                // set line begin char with "|"
                string toLine = "|";
                // loop each value of the row 
                for (int l = 0; l < args.GetLength(1); l++)
                {
                    // set the line string join
                    toLine = toLine + args[k, l].PadRight(maxLength[l]) + "|"; 
                    // if is the first column               
                    if (k==0)
                    {
                        // set the second blank line value 
                        blankLine = blankLine + "-".PadRight(maxLength[l],'-') + "|"; 
                    }
                }
                // if is the first column
                if (k==0)
                {
                   // join the second blank column
                   docMd = toLine + Environment.NewLine + blankLine ; 
                }
                else
                {
                   // if not the first column , join not include blankLine 
                   docMd = docMd + Environment.NewLine + toLine ; 
                }
            }
            //var fileName = fName;
            //var fileName = @"output.md";
            // write text to file 
            File.WriteAllText(fName,docMd);   //Encoding.UTF8  
            // show message    
            Console.WriteLine("Wrote to file: {0}", fName);
            Console.WriteLine();

        }


        public static void arrayToHtml(string[,] args,string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: writing Array to Html file ..."); 
            }
            // set a html string variable with html table tag and a new line
            string docHtml="<table>"+Environment.NewLine;
            // set a variable to check if the value is number
            int[] ifNum = new int[args.GetLength(1)];
            //for loop array 2D reference https://stackoverflow.com/questions/8184306/iterate-through-2-dimensional-array-c-sharp                   
            for (int k = 0; k < args.GetLength(0); k++)
            {
                // set the line variable
                string toLine = "";
                // set a variable html begin tag
                string htmlBeginTag = "";
                // set a variable html end tag
                string htmlEndTag = "";
                // set a line with html tr
                toLine = PadSpace(4) + "<tr>" + Environment.NewLine;
                // if is the first line
                if (k==0) 
                {
                    // set the line html begin tag th, means the column tag
                    htmlBeginTag = "\t\t<th"; 
                    // set the line html end tag
                    htmlEndTag = "</th>" + Environment.NewLine; 
                }
                // if not the first column
                else
                {
                    // set the line html begin tag td
                    htmlBeginTag = "\t\t<td"; 
                    // set the line html end tag
                    htmlEndTag = "</td>" + Environment.NewLine; 
                }  
                // loop each value of the row
                for (int l = 0; l < args.GetLength(1); l++)
                {
                    // if is the second line that not the column, is a value
                    if (k==1)
                    {
                        // if is decimal
                        if (decimal.TryParse(args[k, l], out decimal n))
                        {
                           // set to array, mark this location value is number 
                           ifNum[l]=1;                 
                        }
                        else
                        {
                           // set to array, mark this location value is not number 
                           ifNum[l]=0;  
                        }
                    }
                    // if this value mark is not number 
                    if (ifNum[l]==0)
                    {
                      // join the html tag with this value  
                      toLine = toLine + htmlBeginTag + ">" + args[k, l].Trim() + htmlEndTag;  
                    }
                    // if this value mark is number 
                    else
                    {
                       // join the html tag include align=right with this value  
                       toLine = toLine + htmlBeginTag + " align=\"right\">" + args[k, l].Trim() + htmlEndTag; 
                    }                  
                }
                // join the line with new line
                toLine = toLine + PadSpace(4) + "</tr>" + Environment.NewLine;
                // set to html doc
                docHtml = docHtml + toLine ;
                
            }
            // add end table tag
            docHtml = docHtml + "</table>";
            //var fileName = fName;
            //var fileName = @"output.html";
            // write text to file 
            File.WriteAllText(fName,docHtml); //Encoding.UTF8     
            // show message     
            Console.WriteLine("Wrote to file: {0}", fName);
            Console.WriteLine();      
        }

        public static void arrayToJson(string[,] args,string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: writing Array to Json file ..."); 
            }
            // set a variable string json document content with "["
            string docJson = "[" + Environment.NewLine;
            // set a variable to check if the value is number
            int[] ifNum = new int[args.GetLength(1)];
            // set a arary for column         
            String[] columnName = new String[args.GetLength(1)];
            // set a line variable
            string toLine = "";
            // set a join column value variable
            string joinColVal = "";
            //for loop array 2D reference https://stackoverflow.com/questions/8184306/iterate-through-2-dimensional-array-c-sharp                   
            for (int k = 0; k < args.GetLength(0); k++)
            {
                // if is not the first line
                if(k!=0)
                {
                    // set the line with json tag "{" 
                    toLine = PadSpace(2) + "{" + Environment.NewLine;
                }
                // loop every value of the row 
                for (int l = 0; l < args.GetLength(1); l++)
                {
                    // if is the first line
                    if (k==0)
                    {
                        // set the column name to array
                        columnName[l]=args[k, l];
                    }
                    // if not the first line
                    else
                    {
                        //if is the seconde line 
                        if (k==1)
                        {
                            // check if is the number of the value
                            if (decimal.TryParse(args[k, l], out decimal n))
                            {
                                // if is the number set the variable is 1 of the array location
                                ifNum[l]=1;                 
                            }
                            else
                            {
                                // if not the number set the variable is 0 of the array location
                                ifNum[l]=0;  
                            }
                        }
                        // check if the value of the array location not the number
                        if (ifNum[l]==0)
                        {
                            // joint column name and the value with json tag
                            joinColVal =  PadSpace(4) + "\"" + columnName[l] + "\":\"" + args[k, l].Trim() + "\"" ;  
                            // if not the first value of the row array
                            if (l!=0)
                            {
                                // joint the previous value with "," and new line and column name
                                toLine = toLine + "," + Environment.NewLine + joinColVal; 
                            }     
                            // if is the first value of the row array
                            else
                            {
                                // joint with column name
                                toLine = toLine +  joinColVal;
                            }
                        }  
                        //  check if the value of the array location is the number            
                        else
                        {
                            //joint column name and the value with json tag
                            joinColVal =  PadSpace(4) + "\"" + columnName[l] + "\":" + args[k, l].Trim() ;    
                            // if not the first value of the row array                      
                            if (l!=0)
                            {
                                // joint the previous value with "," and new line and column name
                                toLine = toLine + "," + Environment.NewLine + joinColVal; 
                            }
                            // if is the first value of the row array
                            else
                            {
                                // joint with column name
                                toLine = toLine +  joinColVal;
                            }
                            
                        }  
                    }                
                }
                // if not the first line
                if(k!=0)
                {
                    // join, and add the new line , and space, and "}"
                    toLine = toLine + Environment.NewLine + PadSpace(2) + "}"  ;
                    // if not the second line
                    if(k!=1)
                    { 
                        // add "," and add the new line before the line
                        toLine = "," + Environment.NewLine + toLine ;
                    }
                }
            //Environment.NewLine;
                // join json content
                docJson = docJson + toLine;
            }
            // add end json end tag
            docJson = docJson + Environment.NewLine  + "]";
            //Console.WriteLine(docJson);
            // write text to file 
            File.WriteAllText(fName,docJson); //Encoding.UTF8 
            // show message         
            Console.WriteLine("Wrote to file: {0}", fName);
            Console.WriteLine();  

            /* 
            using JsonDocument jsonFileDoc = JsonDocument.Parse(docJson);
            JsonElement rootJSON = jsonFileDoc.RootElement;
            var fileName = fName;
            //var fileName = @"output.json";
            using FileStream fs = File.OpenWrite(fileName);
            using var writer = new Utf8JsonWriter(fs,
                new JsonWriterOptions { Indented = true });
            jsonFileDoc.WriteTo(writer);
            Console.WriteLine("Wrote to file: {0}", fileName);
            Console.WriteLine();
            */
        }

        public static void arrayToTxt(string[,] args,string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: writing Array to Md file ..."); 
            }
            // set a document content variable
            string docTxt = "";
            // set a array for get the max char length of the column
            int[] maxLength = new int[args.GetLength(1)];
            //for loop array 2D reference https://stackoverflow.com/questions/8184306/iterate-through-2-dimensional-array-c-sharp                              
            for (int k = 0; k < args.GetLength(0); k++)
            {
                // loop each value of the row 
                for (int l = 0; l < args.GetLength(1); l++)
                {
                   // if the value > max length of this column
                   if (args[k, l].Length>maxLength[l])
                   {
                       // set the value to the max length of this column
                       maxLength[l]=args[k, l].Length;
                   }
                }
            }
            // set the no value line begin with "|"
            String blankLine = "+";
            // loop every column
            for (int k = 0; k < args.GetLength(0); k++)
            {
                // set line begin char with "|"
                string toLine = "|";
                // loop each value of the row 
                for (int l = 0; l < args.GetLength(1); l++)
                {
                    // set the line string join
                    if(k==0)
                    {
                        blankLine = blankLine + "-".PadRight(maxLength[l],'-') + "+";
                    }
                    toLine = toLine + args[k, l].PadRight(maxLength[l]) + "|"; 
                }
                // join the second blank column
                docTxt = docTxt + blankLine + Environment.NewLine + toLine + Environment.NewLine ; 
            }
            docTxt = docTxt + blankLine;
            //var fileName = fName;
            //var fileName = @"output.md";
            // write text to file 
            File.WriteAllText(fName,docTxt);   //Encoding.UTF8  
            // show message    
            Console.WriteLine("Wrote to file: {0}", fName);
            Console.WriteLine();

        }

        public static void arrayToTex(string[,] args,string fName,String pVerMod)
        {
            if (pVerMod=="Y")
            {
                Console.WriteLine("debugging output: writing Array to Tex file ..."); 
            }
            // set a document content variable
            string docTex = "\\begin{table}[]" + Environment.NewLine + "\\begin{tabular}{lllll}" ;
            // set a array for get the max char length of the column
            int[] maxLength = new int[args.GetLength(1)];
            //for loop array 2D reference https://stackoverflow.com/questions/8184306/iterate-through-2-dimensional-array-c-sharp                              
            for (int k = 0; k < args.GetLength(0); k++)
            {
                // loop each value of the row 
                for (int l = 0; l < args.GetLength(1); l++)
                {
                   // if the value > max length of this column
                   if (args[k, l].Length>maxLength[l])
                   {
                       // set the value to the max length of this column
                       maxLength[l]=args[k, l].Length;
                   }
                }
            }
            // loop every column
            for (int k = 0; k < args.GetLength(0); k++)
            {
                // set line begin char 
                string toLine = "";
                // loop each value of the row 
                for (int l = 0; l < args.GetLength(1); l++)
                {
                    if (l==0)
                    {
                        // set the line string join
                        toLine = toLine + args[k, l].PadRight(maxLength[l]); 
                    }
                    else
                    {
                        // set the line string join
                        toLine = toLine + " & " + args[k, l].PadRight(maxLength[l]); 
                    }
                }
                // join the line
                if (k==args.GetLength(0)-1)
                {
                    docTex = docTex + Environment.NewLine + toLine  ; 
                }
                else
                {
                    docTex = docTex + Environment.NewLine + toLine  + " \\\\"; 
                }
                
            }
            docTex = docTex + Environment.NewLine + "\\end{tabular}" + Environment.NewLine + "\\end{table}" ;
            //var fileName = fName;
            //var fileName = @"output.md";
            // write text to file 
            File.WriteAllText(fName,docTex);   //Encoding.UTF8  
            // show message    
            Console.WriteLine("Wrote to file: {0}", fName);
            Console.WriteLine();

        }

        // copy from CS264-W02-Demo-Files/myloop/rogram.cs
        public static string PadSpace(int c)
        {
            // Use an empty string to generate c × repeated space character. 
            return "".PadLeft(c);
        }  

}
