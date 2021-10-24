# FormatCovert
 
FormatCovert

- This is a hard-coding.

- Develop an object-oriented console solution (app), called tabconv, for managing and convertion Tablular markup information. 

- This app will provide funcConality to convert between different markup formats for tabular data, for example, it will convert between CSV (Comma Separated Values), MD (Markdown), JSON (JavaScript Object NotaCon, HTML-TABLE (HTML tag <<table>> element) formats, LaTeX tables(https://www.tablesgenerator.com/latex_tables), and text tables (https://www.tablesgenerator.com/text_tables) 
 
## Command-line interface:

          
```
tabconv -v -i <file.ext> -o <file.ext>
        -v, —verbose                    Verbose mode (debugging output to STDOUT)
        -o <file>, —output=<file>       Output file specified by <file>
        -l, —list-formats               List formats
        -h, —help                       Show usage message
        -i, —info                       Show version information
                              
<.ext> will be one of [.html | .md | .csv | .json | .txt | .tex]
  
$ tabconv -i table.json -o table.html (convert JSON table to html tag table )
          
```

