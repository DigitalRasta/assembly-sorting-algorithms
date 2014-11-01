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
				push DWORD PTR [esi+ecx]	   ; swapping by stack because we can't do mov mem,mem
				pop DWORD PTR [esi+ecx+4]
				mov DWORD PTR [esi+ecx], eax
				EndSecondLoopIteration:
					add ecx, 4			   ; incrase iterator by 4 (remeber about 32bit=4bytes)
					jmp SecondLoop
		EndSecondLoop:
		add ebx, 4						   ; incrase iterator by 4 (remeber about 32bit=4bytes)
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
			cmp ecx, 0
			jl EndSecondLoop
			cmp edi, [esi+ecx]
			jnl EndSecondLoop
			push DWORD PTR [esi+ecx]	   ; swapping by stack because we can't do mov mem,mem
			pop DWORD PTR [esi+ecx+4]
			sub ecx, 4
			jmp SecondLoop
		EndSecondLoop:
			mov [esi+ecx+4], edi
			add ebx, 4
			jmp FirstLoop
	EndSort:
	ret
asm_insert endp

end 