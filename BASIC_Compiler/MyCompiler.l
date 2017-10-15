%option noyywrap

%{
#include <stdio.h>
#include <assert.h>
#include <string.h>
#include "MyCompiler.h"
%}

%%
[0-9]+                  {
  printf("Number");
}

LET {
  printf("Assignment statement");
}

%%
