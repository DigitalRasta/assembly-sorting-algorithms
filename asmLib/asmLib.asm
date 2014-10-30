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
;esi - first loop condition
;edi - second loop condition
;edx - addressing
asm_bubble proc uses ecx ebx eax edx esi edi pointer:DWORD, len:DWORD
	xor ebx, ebx ; first loop iterator
	xor ecx, ecx ; second loop iterator
	mov eax, 4
	mul len
	mov esi, eax ; new len value
	mov edx, pointer
	FirstLoop:
		mov eax, esi
		sub eax, ebx
		jng EndSort
		SecondLoopPrepare:
			mov eax, esi
			sub eax, 4
			sub eax, ebx
			mov edi, eax
			xor ecx, ecx
			SecondLoop:
				mov edx, pointer
				mov eax, edi
				sub eax, ecx
				jng EndSecondLoop
				add edx, ecx
				mov eax, DWORD PTR [edx]
				sub eax, DWORD PTR [edx+4]
				jl EndSecondLoopIteration
				mov eax, DWORD PTR [edx+4]
				push DWORD PTR [edx]
				pop DWORD PTR [edx+4]
				mov DWORD PTR [edx], eax
				EndSecondLoopIteration:
					add ecx, 4
					jmp SecondLoop
		EndSecondLoop:
		add ebx, 4
		jmp FirstLoop
	EndSort:
	ret
asm_bubble endp

end 