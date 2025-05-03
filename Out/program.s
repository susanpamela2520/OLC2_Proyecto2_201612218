.data
heap: .space 4096
.text
.global _start
_start:
	adr x10, heap
	// ==== DECLARACION FUNCION ====
	// ========== Bloque ===========
	// ========== Declaración: saludo ===========
	// --------- Primitivo ---------
	// --------- Cadena ---------
	str x10, [sp, #-8]!
	mov w0, #104
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #111
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #108
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #97
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #0
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	// ------- Fin Cadena -------
	// ------- Fin Primitivo -------
	// ========== Fin Declaración ===========
	// =========== Print ===========
	// ========== Acceso a Variable: saludo ===========
	mov x0, #0
	add x0, sp, x0
	ldr x0, [x0, #0]
	str x0, [sp, #-8]!
	// ========== Fin Acceso Variable: ===========
	ldr x0, [sp], #8
	mov x0, x0
	bl print_string
	bl print_space
	// --------- Primitivo ---------
	// --------- Cadena ---------
	str x10, [sp, #-8]!
	mov w0, #109
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #117
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #110
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #100
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #111
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #0
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	// ------- Fin Cadena -------
	// ------- Fin Primitivo -------
	ldr x0, [sp], #8
	mov x0, x0
	bl print_string
	bl print_new_line
	// ========= Fin Print =========
	// ---------- Removiendo Bytes de la Pila ----------
	mov x0, #8
	add sp, sp, x0
	// ------------ Stack Pointer Ajustado -------------
	// ======== Fin Bloque =========
	// == FIN DECLARACION FUNCION ==
	mov x0, #0
	mov x8, #93
	svc #0


// libreria estandar

//--------------------------------------------------------------
// print_string - Prints a null-terminated string to stdout
//
// Input:
//   x0 - The address of the null-terminated string to print
//--------------------------------------------------------------
print_string:
    // Save link register and other registers we'll use
    stp     x29, x30, [sp, #-16]!
    stp     x19, x20, [sp, #-16]!

    // x19 will hold the string address
    mov     x19, x0

print_loop:
    // Load a byte from the string
    ldrb    w20, [x19]

    // Check if it's the null terminator (0)
    cbz     w20, print_done

    // Prepare for write syscall
    mov     x0, #1              // File descriptor: 1 for stdout
    mov     x1, x19             // Address of the character to print
    mov     x2, #1              // Length: 1 byte
    mov     x8, #64             // syscall: write (64 on ARM64)
    svc     #0                  // Make the syscall

    // Move to the next character
    add     x19, x19, #1

    // Continue the loop
    b       print_loop

print_done:
    // Restore saved registers
    ldp     x19, x20, [sp], #16
    ldp     x29, x30, [sp], #16
    ret
    // Return to the caller


//--------------------------------------------------------------
// print_space - Prints a space
//--------------------------------------------------------------
print_space:
    // Print space
    mov x0, #1
    adr x1, espacio
    mov x2, #1
    mov x8, #64
    svc #0
    ret


//--------------------------------------------------------------
// print_new_line - Prints a new line
//--------------------------------------------------------------
print_new_line:
    // Print new line
    mov x0, #1
    adr x1, new_line
    mov x2, #1
    mov x8, #64
    svc #0
    ret

new_line: .ascii "\n"
espacio: .ascii " "