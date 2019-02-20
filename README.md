# Markdown Tools

An attempt to propertly parse Markdown into an AST so it can be output to other formats, most likely HTML.

## WIP - Notes

Will need to work out precedence as I go along. Figured out that this order matters so far:

* CodeBlockIndented
* Heading
* WhiteSpace

Reckon that with recursion and the attributes, something robust can be created. Need to think about it a bit more.