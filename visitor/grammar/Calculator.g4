grammar Calculator;
expression: operand (OPERATOR operand)+;

operand: DIGIT | LPAREN operand (OPERATOR operand)+ RPAREN;

LPAREN: '(';
RPAREN: ')';

OPERATOR: ADD | SUBTRACT | MULTIPLY | DIVIDE;

ADD: '+';
SUBTRACT: '-';
MULTIPLY: '*';
DIVIDE: '/';

DIGIT: [0-9]+;
WS : [ \t\r\n]+ -> skip ; // skip spaces, tabs, newlines