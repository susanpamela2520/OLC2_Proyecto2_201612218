.data
heap: .space 4096
.text
.global _start
_start:
	adr x10, heap
	// ========== Bloque ===========
	// =========== Print ===========
	// ----- Relacional (==) -----
	// --------- Primitivo ---------
	// --------- Entero ---------
	mov x0, #3
	str x0, [sp, #-8]!
	// ------- Fin Entero -------
	// ------- Fin Primitivo -------
	// --------- Primitivo ---------
	// --------- Entero ---------
	mov x0, #4
	str x0, [sp, #-8]!
	// ------- Fin Entero -------
	// ------- Fin Primitivo -------
	ldr x0, [sp], #8
	ldr x1, [sp], #8
	cmp x1, x0 
	beq  L0
	mov x0, #0
	str x0, [sp, #-8]!
	b L1
L0:
	mov x0, #1
	str x0, [sp, #-8]!
L1:
	// --- Fin Relacionales (==) ---
	ldr x0, [sp], #8
	bl print_bool
	bl print_new_line
	// ========= Fin Print =========
	// =========== Print ===========
	// --------- Primitivo ---------
	// -------- Booleano --------
	mov x0, #0
	str x0, [sp, #-8]!
	// ------ Fin Booleano ------
	// ------- Fin Primitivo -------
	ldr x0, [sp], #8
	bl print_bool
	bl print_new_line
	// ========= Fin Print =========
	// =========== Print ===========
	// --------- Primitivo ---------
	// ---------- Rune ----------
	mov x0, #64
	str x0, [sp, #-8]!
	// -------- Fin Rune --------
	// ------- Fin Primitivo -------
	ldr x0, [sp], #8
	bl print_rune
	bl print_new_line
	// ========= Fin Print =========
	// =========== Print ===========
	// --------- Primitivo ---------
	// ---------- Rune ----------
	mov x0, #33
	str x0, [sp, #-8]!
	// -------- Fin Rune --------
	// ------- Fin Primitivo -------
	ldr x0, [sp], #8
	bl print_rune
	bl print_new_line
	// ========= Fin Print =========
	// ======== Fin Bloque =========
	mov x0, #0
	mov x8, #93
	svc #0


// Foreign Function


// Standard Library

//--------------------------------------------------------------
// print_bool - Prints a bool
//--------------------------------------------------------------
print_bool:
    // Print bool
    cmp x0, #1
    bne string_false
    mov x0, #1
    adr x1, true_value
    mov x2, #4
    mov x8, #64
    svc #0
    b exit_print_bool
string_false:
	mov x0, #1
    adr x1, false_value
    mov x2, #5
    mov x8, #64
    svc #0
exit_print_bool:
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


//--------------------------------------------------------------
// print_rune - Prints a rune
//--------------------------------------------------------------
print_rune:
    // Print rune
    sub     sp, sp, #8         // Reservar espacio en el stack
    strb    w0, [sp]           // Escribir solo un byte en el stack
    mov     x0, #1             // stdout
    mov     x1, sp             // dirección del byte
    mov     x2, #1             // longitud
    mov     x8, #64            // syscall write
    svc     #0
    add     sp, sp, #8         // Restaurar el stack
    ret

true_value: .ascii "true"
false_value: .ascii "false"
new_line: .ascii "\n"