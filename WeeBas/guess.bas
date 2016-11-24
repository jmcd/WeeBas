1  REM NUMBER GUESSING GAME
4 print "ENTER UPPER LIMIT FOR GUESS"
5 input l
10 let n = !rnd l
15 let c = 0
20 PRINT "I'VE GOT THE NUMBER."
21 PRINT ""
30 print "WHAT'S YOUR GUESS "
31 input G
35 let C = C + 1
40 IF g > n THEN goto 200
50 IF g<n THEN goto 300
60 print "GREAT! YOU GOT MY NUMBER IN ONLY "
65 print c
67 print "GUESSES."
70 PRINT "DO YOU WANT TO TRY ANOTHER (1/0)?"
80 input a
90 if a = 1 then goto 1
100 if a = 0 then end
110 goto 70
200 rem Number is lower
210 print "MY NUMBER IS LOWER."
220 goto 30
300 rem Number is higher
310 print "MY NUMBER IS HIGHER."
320 goto 30
