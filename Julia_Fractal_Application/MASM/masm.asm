.data	
constTwo REAL8 2.0
constMultiplier REAL8 3.529411764705
constDivider REAL8 0.425
constFour REAL8 4.0

.code

colorBase proc
	PSLLDQ xmm1, 8
	VBLENDPD xmm1, xmm1, xmm0, 1		;utworzenie wektora sta³ej C
	MOVSD xmm4, QWORD PTR[rsp+40]
	MOVSD xmm5, QWORD PTR[rsp+48]
	CALL declaration					;utworzenie i przypisanie Z
	;C - xmm1, Z - xmm0
	MOV ecx, 100
	myLoop:
		CALL pow
		CALL loopCondition
		MOVSD xmm6, constFour
		VSUBPD xmm6, xmm6, xmm2
		JAE endLoop
		LOOP myLoop
	endLoop:
		MOV eax, 100
		SUB eax, ecx
		ret
colorBase endp

declaration proc
	MOVSD xmm6, constTwo				;2.0 do pamiêci operacyjnej
	MOVSD xmm7, constMultiplier
	MOVSD xmm8, constDivider

	DIVPD xmm3, xmm6					;Y / 2.0
	SUBPD xmm5, xmm3					;y-Y/2
	MULPD xmm3, xmm6					;powrót wartoœci Y
	DIVPD xmm5, xmm8					;(y-Y/2)/0.425
	DIVPD xmm5, xmm3					;(y-Y/2)/(0.425*Y)

	
	DIVPD xmm2, xmm6					;X / 2.0
	SUBPD xmm4, xmm2					;x-X/2
	MULPD xmm2, xmm6					;powrót wartoœci X
	MULPD xmm4, xmm7					;(x-X/2)*3.529411764705
	DIVPD xmm4, xmm2					;((x-X/2)*3.529411764705)/X

	
	PSLLDQ xmm5, 8
	VBLENDPD xmm0, xmm5, xmm4, 1		;przypisanie utworzonych wartoœci do xmm0 reprezentuj¹cego liczbê zespolon¹ Z
	RET
declaration endp

pow proc	
	MOVSD xmm4, constTwo
	VMOVAPD	xmm2, xmm0					;przekopiowanie xmm1 do xmm0
	MOVHLPS	xmm3, xmm0					;dolne wartoœæ xmm0 przepisane do xmm2
	MULPD xmm2, xmm4					;przemno¿enie xmm0 razy dwa
	MULPD xmm2, xmm3					;pomno¿enie xmm0 i xmm2
	VMULPD	xmm0, xmm0, xmm0			;wektorowe podniesienie do kwadratu zawartoœci xmm1
	HSUBPD	xmm0, xmm0					;horyzontalne odjêcie od siebie liczb w xmm1
	PSLLDQ xmm2, 8
	VBLENDPD xmm0, xmm2, xmm0, 1		;przeniesienie do dolnej wartoœci xmm1 górnej wartoœci xmm0
	RET
pow endp

loopCondition proc
;	MOVSD QWORD ptR[rsp+40], xmm0
;	MOVSD QWORD ptR[rsp+40], xmm1
	VMULPD	xmm2, xmm0, xmm0
	HADDPD	xmm2, xmm2
	RET
loopCondition endp
end