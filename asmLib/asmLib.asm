.686 
.model flat, stdcall 
.xmm
.data
.code

;Description:
; Check if asm DLL is properly loaded
;Author: Jakub'Digitalrasta'Bujny
;Version: 0.0.0
;Changelog:
;Arguments:
;Registers:
;Return: void
asm_testDll proc
	ret
asm_testDll endp

;Description:
; Basic implementation of bubble sort
;Author: Jakub'Digitalrasta'Bujny
;Version: 0.1.0
;Changelog:
;	0.0.0: basic implementation of bubble sort
;	0.1.0: changed method of elements swapping (pushing edx on stack)
;Arguments:
;pointer - pointer to array. Results will be stored here. Original data will be overwritten
;len - length of array
;Registers:
;eax - computing
;ebx - first loop iterator
;ecx - second loop iterator
;edx - first loop condition
;edi - second loop condition
;esi - addressing
;Return: void
asm_bubble proc uses ecx ebx eax esi edx edi pointer:DWORD, len:DWORD
	xor ebx, ebx                           ; clear first loop iterator
	mov eax, 4
	mul len		                           ; integer is 32bit = 4 bytes, so multiply length of array by 4
	mov edx, eax						   ; store new length value
	mov esi, pointer					   ; store pointer in register for addressing
	FirstLoop:
		push edx						   ; push first loop condition on stack because edx is needed in swapping
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
				jl EndSecondLoopIteration      ; jump if not
				mov eax, DWORD PTR [esi+ecx+4] ; case: currentElement > nextElement, we must swap
				mov edx, DWORD PTR [esi+ecx]   ; swapping
				mov DWORD PTR [esi+ecx+4], edx
				mov DWORD PTR [esi+ecx], eax
				EndSecondLoopIteration:
					add ecx, 4			   ; incrase iterator by 4 (remeber about 32bit=4bytes)
					jmp SecondLoop
		EndSecondLoop:
		add ebx, 4						   ; incrase iterator by 4 (remeber about 32bit=4bytes)
		pop edx							   ; restore first loop condition
		jmp FirstLoop
	EndSort:
	pop edx								   ; cleaning on end
	ret
asm_bubble endp

;Description:
; Basic implementation of insert sort
;Author: Jakub'Digitalrasta'Bujny
;Version: 0.1.1
;Changelog:
;	0.0.0: basic implementation of insert sort
;	0.1.0: changed method of elements swapping
;	0.1.1: fixed bug while checking loop condition
;Arguments:
;pointer - pointer to array. Results will be stored here. Original data will be overwritten
;len - length of array
;Registers:
;eax - computing
;ebx - first loop iterator
;ecx - second loop iterator
;edx - first loop condition
;edi - temp
;esi - addressing
;Return: void
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
		mov eax, ebx					   ; set second loop iterator in next 3 instructions, secondLoopIterator = firstLoopIterator - 4
		sub eax, 4
		mov ecx, eax
		SecondLoop:
			cmp ecx, 0					   ; check first condition, end if second loop iterator < 0
			jl EndSecondLoop
			cmp edi, [esi+ecx]			   ; check second condtion, end if edi(temp) >= pointer[ecx] 
			jnl EndSecondLoop
			mov eax, DWORD PTR [esi+ecx]	   ;swapping
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


;Description:
; Basic implementation of recursive quick sort
;Author: Jakub'Digitalrasta'Bujny
;Version: 0.0.1
;Changelog:
;	0.0.0: basic implementation of quick sort
;	0.0.1: fixed bug in elements swapping
;Arguments:
;pointer - pointer to array. Results will be stored here. Original data will be overwritten
;lowp - low index of array (divide and conquer)
;highp - high index of array (divide and conquer)
;Registers:
;eax - computing
;ecx - i
;ebx - j
;edx - divide
;edi - temp
;esi - addressing
;Return: void
asm_quick_rec proc uses ecx ebx eax edx esi edi pointer:DWORD, lowp:DWORD, highp:DWORD
	mov ecx, lowp			   ;insert low index of array
	mov ebx, highp			   ;insert high index of array
	cmp ecx, ebx			   ;if low < high : end of sorting
	jge EndSort
	mov esi, pointer		   ;array indexing
	mov edx, lowp			   ;set divide as low index
	MainLoop:
		cmp ecx, ebx		   ;if i >= j : end of main loop
		jge EndMainLoop
		FirstLoop:
			mov eax, [esi+ecx] ;comapring elements indexed by ecx (i) with element indexed by point of divide (edx)
			cmp eax, [esi+edx] ;if array[i] > array[divide] : end first loop
			jg SecondLoop
			mov eax, highp
			cmp eax, ecx       ; if i >= high : end first loop
			jng SecondLoop
			add ecx, 4		   ; increase iterator
			jmp FirstLoop      ; loop
		SecondLoop:
			mov eax, [esi+ebx]
			cmp eax, [esi+edx] ; if array[j] <= array[divide] : end second loop
			jng EndSecondLoop
			sub ebx, 4         ; decrease iterator (j)
			jmp SecondLoop	   ; loop
		EndSecondLoop:
		cmp ecx, ebx		   ; if i >= j : end of main loop
		jge EndMainLoop
		mov edi, [esi+ecx]     ; swapping elements indexed by iterators i and j
		mov eax, [esi+ebx]
		mov [esi+ecx], eax
		mov [esi+ebx], edi
		jmp MainLoop		   ; loop
	EndMainLoop:
		mov edi, [esi+edx]     ; swapping elements indexed by divide and iterator j
		mov eax, [esi+ebx]
		mov [esi+edx], eax
		mov [esi+ebx], edi
		mov eax, ebx		   ; decrease iterator j
		sub eax, 4
		invoke asm_quick_rec, pointer, lowp, eax ; recursive invoke
		mov eax, ebx		   ; incrase iterator 
		add eax, 4
		invoke asm_quick_rec, pointer, eax, highp ; recursive invoke
	EndSort:
		ret
asm_quick_rec endp
 
;Description:
; method used to invoke main quick sort alghorithm
;Author: Jakub'Digitalrasta'Bujny
;Version: 0.0.0
;Changelog:
;Arguments:
;pointer - pointer to array. Results will be stored here. Original data will be overwritten
;len - length of array
;Registers:
;eax - computing
;ebx - used in multiplication
;Return: void
asm_quick proc uses eax ebx pointer:DWORD, len:DWORD
	mov eax, len ; prepare recursive conditions (length - 1)
	sub eax, 1
	mov ebx, 4   ; remeber about 4x8B integers
	mul ebx
	invoke asm_quick_rec, pointer, 0, eax
	ret
asm_quick endp

end