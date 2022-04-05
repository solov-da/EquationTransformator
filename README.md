# Equation Transformator

Project with samples my code. 
Console application for transforming equation into canonical form.

## Description

Console application with interactive/file mode. 
File mode is enabled by passing the -file argument, 
out file is saved in the same directory where is the input file.

The equation will be given in the following form:
P1 + P2 + ... = ... + PN
where P1..PN - summands, which look like:
ax^k
where a - floating point value;
k - integer value;
x - variable (each summand can have many variables).

For example:
x^2 + 3.5xy + y = y^2 - xy + y
Should be transformed into:
x^2 - y^2 + 4.5xy = 0

## Examples of running an application in interactive mode

Enter equation:
---2(-x+y) = 2(y-3)z

Canonical equation:
2x - 2y - 2yz + 6z = 0


Enter equation:
---0(-x+y) = 2(y-3)z

Canonical equation:
- 2yz + 6z = 0


Enter equation:
---0(-x+y) = 2((-1)(y-3)(-1))z

Canonical equation:
- 2yz + 6z = 0


Enter equation:
xxx + y^2 = (yy + 1)(-2 + z)

Canonical equation:
x^3 + 3y^2 - y^2z - z + 2 = 0