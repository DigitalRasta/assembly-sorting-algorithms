.686 
.387
.model flat, stdcall 
.xmm
.data
.code

asm_testDll proc
	ret
asm_testDll endp

asm_bubble proc uses ecx pointer:DWORD, len:DWORD
	mov ecx, pointer;
	add DWORD PTR [ecx], 8
	add DWORD PTR [ecx+4], 8
	ret
asm_bubble endp

end 