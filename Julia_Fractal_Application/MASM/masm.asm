.data	
constTwo REAL8 2.0
constMultiplier REAL8 3.529411764705
constDivider REAL8 0.425
constFour REAL8 4.0

.code
colorBaseAsm proc
	MOVUPD XMMWORD PTR[rsp-32], xmm7
	MOVUPD XMMWORD PTR[rsp-16], xmm8

	PSLLDQ xmm1, 8
	VBLENDPD xmm1, xmm1, xmm0, 1		;utworzenie wektora sta³ej C
	MOVSD xmm4, QWORD PTR[rsp+40]
	MOVSD xmm5, QWORD PTR[rsp+48]
	CALL declaration					;utworzenie i przypisanie Z
	MOV ecx, 100
	myLoop:
		CALL pow
		CALL loopCondition
		MOVSD xmm3, constFour
		SUBPD xmm3, xmm2
		PSLLDQ xmm3, 8
		VMOVMSKPD rax, xmm3
		SUB rax, 2
		JZ endLoop
		LOOP myLoop
	endLoop:
		MOV eax, 100
		SUB eax, ecx
		MOVUPD xmm7, XMMWORD PTR[rsp-16]
		MOVUPD xmm8, XMMWORD PTR[rsp-32]
		RET
colorBaseAsm endp

declaration proc
	MOVSD xmm6, constTwo				;sta³e do pamiêci operacyjnej
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
	VMOVAPD	xmm2, xmm0					;przekopiowanie liczby Z do pomocniczego rejestru xmm2
	MOVHLPS	xmm3, xmm0					;czêœæ urojona Z kopiowana do xmm3
	MULPD xmm2, xmm4					;przemno¿enie rzeczywistej czêœci razy 2
	MULPD xmm2, xmm3					;przemno¿enie czêœci rzeczywistej i urojonej
	VMULPD xmm0, xmm0, xmm0				;
	HSUBPD xmm0, xmm0					;
	PSLLDQ xmm2, 8						;
	VBLENDPD xmm0, xmm2, xmm0, 1		;
	VADDPD xmm0, xmm0, xmm1
	RET
pow endp

loopCondition proc
	VMULPD	xmm2, xmm0, xmm0
	HADDPD	xmm2, xmm2
	RET
loopCondition endp
end