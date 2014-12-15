.686 
.model flat, stdcall 
.xmm
.data
.code

asm_testDll proc
	ret
asm_testDll endp

;eax - computing
;ebx - first loop iterator
;ecx - second loop iterator
;edx - first loop condition
;edi - second loop condition
;esi - addressing
asm_bubble proc uses ecx ebx eax esi edx edi pointer:DWORD, len:DWORD
	xor ebx, ebx                           ; clear first loop iterator
	mov eax, 4
	mul len		                           ; integer is 32bit = 4 bytes, so multiply length of array by 4
	mov edx, eax						   ; store new length value
	mov esi, pointer					   ; store pointer in register for addressing
	FirstLoop:
		push edx				       
		cmp edx, ebx					   ; check loop condition, edx=len*4, ebx is iterator
		jng EndSort
		SecondLoopPrepare:
			mov eax, edx			       ; prepare second loop condition: len*4 - 4 - ebx (in C its: length-1-i)
			sub eax, 4
			sub eax, ebx
			mov edi, eax
			xor ecx, ecx				   ; clear second loop iterator
			SecondLoop:
				mov esi, pointer           ; restore pointer             
				cmp edi, ecx			   ; check loop condition
				jng EndSecondLoop
				mov eax, DWORD PTR [esi+ecx]   ; check if currentElement > nextElement
				sub eax, DWORD PTR [esi+ecx+4] ;
				jl EndSecondLoopIteration  ; jump if not
				mov eax, DWORD PTR [esi+ecx+4] ; case: currentElement > nextElement, we must swap
				mov edx, DWORD PTR [esi+ecx] ; swapping
				mov DWORD PTR [esi+ecx+4], edx
				mov DWORD PTR [esi+ecx], eax
				EndSecondLoopIteration:
					add ecx, 4			   ; incrase iterator by 4 (remeber about 32bit=4bytes)
					jmp SecondLoop
		EndSecondLoop:
		add ebx, 4						   ; incrase iterator by 4 (remeber about 32bit=4bytes)
		pop edx
		jmp FirstLoop
	EndSort:
	ret
asm_bubble endp

;eax - computing
;ebx - first loop iterator
;ecx - second loop iterator
;edx - first loop condition
;edi - temp
;esi - addressing
asm_insert proc uses ecx ebx eax edx esi edi pointer:DWORD, len:DWORD
	mov ebx, 4                             ; set first loop iterator
	mov eax, 4
	mul len		                           ; integer is 32bit = 4 bytes, so multiply length of array by 4
	mov edx, eax						   ; store new length value
	mov esi, pointer					   ; store pointer in register for addressing
	FirstLoop:
					       
		cmp edx, ebx					   ; check loop condition, edx=len*4, ebx is iterator
		jng EndSort
		mov edi, [esi+ebx]				   ; store pointer[ebx] in temp
		mov eax, ebx					   ; set second loop iterator in next 3 instructions
		sub eax, 4
		mov ecx, eax
		SecondLoop:
			cmp ecx, 0					   ; check first condition, end if second loop iterator < 0
			jl EndSecondLoop
			cmp edi, [esi+ecx]			   ; check second condtion, end if edi(temp) >= pointer[ecx] 
			jnl EndSecondLoop
			mov eax, DWORD PTR [esi+ecx]	   ; swapping by stack because we can't do mov mem,mem
			mov DWORD PTR [esi+ecx+4], eax
			sub ecx, 4					   ; decrease second loop iterator
			jmp SecondLoop
		EndSecondLoop:
			mov [esi+ecx+4], edi		   ; mov temp to pointer[iterator+1]
			add ebx, 4
			jmp FirstLoop
	EndSort:
	ret
asm_insert endp



;eax - computing
;ecx - i
;ebx - j
;edx - divide
;edi - temp
;esi - addressing
asm_quick_rec proc uses ecx ebx eax edx esi edi pointer:DWORD, lowp:DWORD, highp:DWORD
	mov ecx, lowp
	mov ebx, highp
	cmp ecx, ebx
	jge EndSort
	mov esi, pointer
	mov edx, lowp
	MainLoop:
		cmp ecx, ebx
		jge EndMainLoop
		FirstLoop:
			mov eax, [esi+ecx]
			cmp eax, [esi+edx]
			jg SecondLoop
			mov eax, highp
			cmp eax, ecx
			jng SecondLoop
			add ecx, 4
			jmp FirstLoop
		SecondLoop:
			mov eax, [esi+ebx]
			cmp eax, [esi+edx]
			jng EndSecondLoop
			sub ebx, 4
			jmp SecondLoop
		EndSecondLoop:
		cmp ecx, ebx
		jge MainLoop
		mov edi, [esi+ecx]
		mov eax, [esi+ebx]
		mov [esi+ecx], eax
		mov [esi+ebx], edi
		jmp MainLoop
	EndMainLoop:
		mov edi, [esi+edx]
		mov eax, [esi+ebx]
		mov [esi+edx], eax
		mov [esi+ebx], edi
		mov eax, ebx
		sub eax, 4
		invoke asm_quick_rec, pointer, lowp, eax
		mov eax, ebx
		add eax, 4
		invoke asm_quick_rec, pointer, eax, highp
	EndSort:
		ret
asm_quick_rec endp
 

asm_quick proc uses eax ebx pointer:DWORD, len:DWORD
	mov eax, len
	sub eax, 1
	mov ebx, 4
	mul ebx
	invoke asm_quick_rec, pointer, 0, eax
	ret
asm_quick endp

end