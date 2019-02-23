# Markdown Tools

An attempt to propertly parse Markdown into an AST so it can be output to other formats, most likely HTML.

## WIP - Notes

Will need to work out precedence as I go along. Figured out that this order matters so far:

* CodeBlockIndented
* Heading
* Everything Else
* WhiteSpace
* Text

## A note on CheckEvaluatorAttributes

For now, assumes that any item can start a document so ValidPreviousNodeSequenceAttribute attributes are ignored in that case.