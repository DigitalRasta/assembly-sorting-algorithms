.686 
.model flat, stdcall 
.xmm
.data
.code

asm_testDll proc
	ret
asm_testDll endp

asm_bubble proc uses ecx ebx eax edx esi edi pointer:DWORD, len:DWORD
	mov eax, DWORD PTR [pointer]
	mov eax, DWORD PTR [pointer+1]
	mov eax, DWORD PTR [pointer+2]
	mov eax, DWORD PTR [pointer+3]
	mov eax, DWORD PTR [pointer+4]
	mov eax, DWORD PTR [pointer+5]
	mov eax, DWORD PTR [pointer+6]
	mov eax, DWORD PTR [pointer+7]
	mov eax, DWORD PTR [pointer+8]
	mov eax, DWORD PTR [pointer+9]
	mov eax, DWORD PTR [pointer+10]
	mov eax, DWORD PTR [pointer+11]
	mov eax, DWORD PTR [pointer+12]
	mov ebx, 4
	mov ecx, 4 ; second loop iterator
	mov eax, 4
	mul len
	mov esi, eax ; new len value
	add esi, 4
	FirstLoop:
		mov eax, esi
		sub eax, ebx
		jng EndSort
		SecondLoopPrepare:
			mov eax, esi
			sub eax, 4
			sub eax, ebx
			mov edi, eax
			SecondLoop:
				mov eax, edi
				sub eax, ecx
				jng EndSecondLoop
				mov eax, DWORD PTR [pointer+ecx+4]
				sub eax, DWORD PTR [pointer+ecx+4]
				jl EndSecondLoopIteration
				mov edx, DWORD PTR [pointer+ecx+4]
				mov eax, DWORD PTR [pointer+ecx]
				mov DWORD PTR [pointer+ecx+4], eax
				mov DWORD PTR [pointer+ecx], edx
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