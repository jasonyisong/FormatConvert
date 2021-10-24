// this program created by Yi Song 2021/9/25
// It will convert between CSV (Comma Sepa- rated Values), MD (Markdown), JSON (JavaScript Object NotaCon, and HTML-TABLE (HTML <table> element) formats. 
// comment the code update by Yi Song 2021/10/5
// add the .txt and .tex method update by Yi Song 2021/10/6

using System;
using System.IO;

namespace assignment_01
{
    class Program
    {
        static void Main(string[] args)
        {
            // set a Verbose mode variable 
            String verMode="N";
            // set a file path variable
            string filePath="";
            // set a input file variable
            String inputFile="";
            // set a input file Extension variable
            String inputFileExt="";
            // set a output file variable
            String outputFile="";
            // set a output file Extension variable
            String outputFileExt="";
            // set a lower Case variable
            String lowerCaseParameter = "";
            
            // loop to get the input variable
            for (int y = 0; y <= args.Length-1; y++)
            {
                // transfer the paramter to low case
                lowerCaseParameter=args[y].ToLower();
                // switch the variable
                switch (lowerCaseParameter)
                {
                    // if is -v
                    case "-v":     
                        // set Verbose mode value is Y
                        verMode="Y";
                        break;   
                    // if is -o  
                    case "-o":
                        // set file path value is follow variable value y+1
                        filePath = args[y+1];
                        // get the path file Extension
                        outputFileExt = Path.GetExtension(filePath); 
                        // if the Extension is .html or .md or .csv or .json
                        if (outputFileExt==".html" | outputFileExt==".md" | outputFileExt==".csv" | outputFileExt==".json" | outputFileExt==".txt" | outputFileExt==".tex")
                        {
                            // set file path to outputFile variable
                            outputFile = filePath;
                            //Console.WriteLine("File Extension: {0}", outputFileExt); 
                        }  
                        // skip the follow variable
                        y=y+1;
                        break;  
                    // if is -l  
                    case "-l": 
                        // show message     
                        Console.WriteLine(@"<.ext> will be one of [.html | .md | .csv | .json | .txt | .tex]");  
                        break; 
                    // if is -h                                            
                    case "-h":  
                        // show message      
                        Console.WriteLine(@"tabconv  -v -i <file.ext> -o <file.ext>");
                        Console.WriteLine(@"");
                        Console.WriteLine(@"    -v, —verbose                        Verbose mode (debugging output to STDOUT)");
                        Console.WriteLine(@"    -o, <file>, —output=<file>          Output file specified by <file>");
                        Console.WriteLine(@"    -l, —list-formats                   List formats");
                        Console.WriteLine(@"    -h, —help                           Show usage message");
                        Console.WriteLine(@"    -i, —info                           Show version information");
                        Console.WriteLine(@"");
                        Console.WriteLine(@"<.ext> will be one of [.html | .md | .csv | .json | .txt | .tex]");  
                        Console.WriteLine(@"");
                        break;
                    // if is -i
                    case "-i":   
                        // show message 
                        Console.WriteLine(@"Version 1.0");
                        break;  
                    // the others may be input file                    
                    default:
                        // set the variable value to file path value
                        filePath = lowerCaseParameter;
                        // get the Extension
                        inputFileExt = Path.GetExtension(filePath); 
                        // if the Extension is .html or .md or .csv or .json
                        if (inputFileExt==".html" | inputFileExt==".md" | inputFileExt==".csv" | inputFileExt==".json" | inputFileExt==".txt" | inputFileExt==".tex")
                        {
                            // set file path to inputFile variable
                            inputFile = filePath;
                            //Console.WriteLine("File Extension: {0}", ext); 
                        }                 
                        break;
                }


                //Console.WriteLine(outputFile);
            }
            if (verMode=="Y")
            {
                Console.WriteLine("debugging output: checking file name and extension ..."); 
            }
            // if output file is not null or empty
            if (String.IsNullOrEmpty(outputFile)==false)
            {
                // if input file is null or empty or not exist, and output file Extension is not equal input file Extension 
                if ( File.Exists(inputFile)==false | String.IsNullOrEmpty(inputFile) | outputFileExt==inputFileExt)
                {
                    // show message
                    Console.WriteLine("[.html | .md | .csv | .json | .txt | .tex] Input or Output File is Empty, or Input file no exist, or Both File name Extension is wrong or same, Pls try another file format");
                }
                else
                {  
                    if (verMode=="Y")
                    {
                        Console.WriteLine("debugging output: ready to convert file format ..."); 
                    }
                    try
                    {
                        // switch input file Extension
                        switch (inputFileExt)
                        {
                                // if is .html
                                case ".html":
                                    // switch output file Extension
                                    switch (outputFileExt)
                                    {
                                        case ".md":
                                            // call tabcular.cs first to covert file to array, and array to md file
                                            tabular.arrayToMd(tabular.HtmlFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".csv":
                                            // call tabcular.cs first to covert file to array, and array to csv file
                                            tabular.arrayToCsv(tabular.HtmlFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".json":
                                            // call tabcular.cs first to covert file to array, and array to md file
                                            tabular.arrayToJson(tabular.HtmlFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".txt":
                                            // call tabcular.cs first to covert file to array, and array to txt file
                                            tabular.arrayToTxt(tabular.HtmlFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".tex":
                                            // call tabcular.cs first to covert file to array, and array to tex file
                                            tabular.arrayToTex(tabular.HtmlFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        default: 
                                            break;
                                    }
                                    break; 
                                // if is .md 
                                case ".md":
                                    switch (outputFileExt)
                                    {
                                        case ".html":
                                            // call tabcular.cs first to covert file to array, and array to html file
                                            tabular.arrayToHtml(tabular.MdFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".csv":
                                            // call tabcular.cs first to covert file to array, and array to csv file
                                            tabular.arrayToCsv(tabular.MdFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".json":
                                            // call tabcular.cs first to covert file to array, and array to json file
                                            tabular.arrayToJson(tabular.MdFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".txt":
                                            // call tabcular.cs first to covert file to array, and array to txt file
                                            tabular.arrayToTxt(tabular.MdFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".tex":
                                            // call tabcular.cs first to covert file to array, and array to tex file
                                            tabular.arrayToTex(tabular.MdFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        default: 
                                            break;
                                    }
                                    break; 
                                //if is csv
                                case ".csv":
                                    switch (outputFileExt)
                                    {
                                        case ".html":
                                            // call tabcular.cs first to covert file to array, and array to html file
                                            tabular.arrayToHtml(tabular.CsvFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".md":
                                            // call tabcular.cs first to covert file to array, and array to md file
                                            tabular.arrayToMd(tabular.CsvFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".json":
                                            // call tabcular.cs first to covert file to array, and array to json file
                                            tabular.arrayToJson(tabular.CsvFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".txt":
                                            // call tabcular.cs first to covert file to array, and array to txt file
                                            tabular.arrayToTxt(tabular.CsvFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".tex":
                                            // call tabcular.cs first to covert file to array, and array to tex file
                                            tabular.arrayToTex(tabular.CsvFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        default: 
                                            break;
                                    }
                                    break;
                                //if is .json
                                case ".json":
                                    switch (outputFileExt)
                                    {
                                        case ".html":
                                            // call tabcular.cs first to covert file to array, and array to html file
                                            tabular.arrayToHtml(tabular.JsonFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".md":
                                            // call tabcular.cs first to covert file to array, and array to md file
                                            tabular.arrayToMd(tabular.JsonFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".csv":
                                            // call tabcular.cs first to covert file to array, and array to csv file
                                            tabular.arrayToCsv(tabular.JsonFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".txt":
                                            // call tabcular.cs first to covert file to array, and array to txt file
                                            tabular.arrayToTxt(tabular.JsonFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".tex":
                                            // call tabcular.cs first to covert file to array, and array to tex file
                                            tabular.arrayToTex(tabular.JsonFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        default: 
                                            break;
                                    }
                                    break; 
                                    //if is .txt
                                case ".txt":
                                    switch (outputFileExt)
                                    {
                                        case ".html":
                                            // call tabcular.cs first to covert file to array, and array to html file
                                            tabular.arrayToHtml(tabular.TxtFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".md":
                                            // call tabcular.cs first to covert file to array, and array to md file
                                            tabular.arrayToMd(tabular.TxtFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".csv":
                                            // call tabcular.cs first to covert file to array, and array to csv file
                                            tabular.arrayToCsv(tabular.TxtFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".json":
                                            // call tabcular.cs first to covert file to array, and array to json file
                                            tabular.arrayToJson(tabular.TxtFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".tex":
                                            // call tabcular.cs first to covert file to array, and array to tex file
                                            tabular.arrayToTex(tabular.TxtFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        default: 
                                        break;
                                    }
                                    break; 
                                case ".tex":
                                    switch (outputFileExt)
                                    {
                                        case ".html":
                                            // call tabcular.cs first to covert file to array, and array to html file
                                            tabular.arrayToHtml(tabular.TexFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".md":
                                            // call tabcular.cs first to covert file to array, and array to md file
                                            tabular.arrayToMd(tabular.TexFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".csv":
                                            // call tabcular.cs first to covert file to array, and array to csv file
                                            tabular.arrayToCsv(tabular.TexFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".json":
                                            // call tabcular.cs first to covert file to array, and array to json file
                                            tabular.arrayToJson(tabular.TexFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        case ".txt":
                                            // call tabcular.cs first to covert file to array, and array to txt file
                                            tabular.arrayToTxt(tabular.TexFileReadToArray(inputFile,verMode),outputFile,verMode);
                                            break; 
                                        default: 
                                        break;
                                    }
                                    break; 
                                default: 
                                    break; 
                        }
                    }
                    // if get some exception 
                    catch (Exception e)
                    {
                        // show message
                        Console.WriteLine("{0} Exception caught.", e);
                        Console.WriteLine("Pls check the input file format first, or contact IT Administrator.");
                    }
                   //Console.WriteLine("ok");
                }
            }
            
        }        
    }
}
