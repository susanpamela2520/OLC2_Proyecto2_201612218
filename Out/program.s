.data
heap: .space 4096
.text
.global _start
_start:
	adr x10, heap
	// ==== DECLARACION FUNCION ====
	// ========== Bloque ===========
	// =========== Print ===========
	// --------- Primitivo ---------
	// --------- Cadena ---------
	str x10, [sp, #-8]!
	mov w0, #99
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #105
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #99
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #108
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #111
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #32
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #102
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #111
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #114
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
	// ========== Declaración: n ===========
	// --------- Primitivo ---------
	// --------- Entero ---------
	mov x0, #4
	str x0, [sp, #-8]!
	// ------- Fin Entero -------
	// ------- Fin Primitivo -------
	// ========== Fin Declaración ===========
	// ========== Instruccion While ===========
L0:
	// ----- Relacional (>) -----
	// ========== Acceso a Variable: n ===========
	mov x0, #0
	add x0, sp, x0
	ldr x0, [x0, #0]
	str x0, [sp, #-8]!
	// ========== Fin Acceso Variable: ===========
	// --------- Primitivo ---------
	// --------- Entero ---------
	mov x0, #0
	str x0, [sp, #-8]!
	// ------- Fin Entero -------
	// ------- Fin Primitivo -------
	ldr x0, [sp], #8
	ldr x1, [sp], #8
	cmp x1, x0 
	bgt  L2
	mov x0, #0
	str x0, [sp, #-8]!
	b  L3
L2:
	mov x0, #1
	str x0, [sp, #-8]!
L3:
	str x0, [sp, #-8]!
	// --- Fin Relacionales (>) ---
	ldr x0, [sp], #8
	cbz x0, L1 
	// ========== Bloque ===========
	// ========== Instruccion If ===========
	// ----- Relacional (==) -----
	// ========== Acceso a Variable: n ===========
	mov x0, #8
	add x0, sp, x0
	ldr x0, [x0, #0]
	str x0, [sp, #-8]!
	// ========== Fin Acceso Variable: ===========
	// --------- Primitivo ---------
	// --------- Entero ---------
	mov x0, #2
	str x0, [sp, #-8]!
	// ------- Fin Entero -------
	// ------- Fin Primitivo -------
	ldr x0, [sp], #8
	ldr x1, [sp], #8
	cmp x1, x0 
	beq  L4
	mov x0, #0
	str x0, [sp, #-8]!
	b  L5
L4:
	mov x0, #1
	str x0, [sp, #-8]!
L5:
	str x0, [sp, #-8]!
	// --- Fin Relacionales (==) ---
	ldr x0, [sp], #8
	cbz x0, L6 
	// ========== Bloque ===========
	// Sentencia Break
	b  L1
	// ======== Fin Bloque =========
L6:
	// ========== Fin If ===========
	// =========== Print ===========
	// ========== Acceso a Variable: n ===========
	mov x0, #16
	add x0, sp, x0
	ldr x0, [x0, #0]
	str x0, [sp, #-8]!
	// ========== Fin Acceso Variable: ===========
	ldr x0, [sp], #8
	mov x0, x0
	bl print_integer
	bl print_new_line
	// ========= Fin Print =========
	// ========== Asignacion Variable: n ===========
	// ----- Aritmetica (-) -----
	// ========== Acceso a Variable: n ===========
	mov x0, #16
	add x0, sp, x0
	ldr x0, [x0, #0]
	str x0, [sp, #-8]!
	// ========== Fin Acceso Variable: ===========
	// --------- Primitivo ---------
	// --------- Entero ---------
	mov x0, #1
	str x0, [sp, #-8]!
	// ------- Fin Entero -------
	// ------- Fin Primitivo -------
	ldr x0, [sp], #8
	ldr x1, [sp], #8
	sub x0, x1, x0
	str x0, [sp, #-8]!
	// --- Fin Aritmetica (-) ---
	ldr x0, [sp], #8
	mov x1, #16
	add x1, sp, x1
	str x0, [x1, #0]
	str x0, [sp, #-8]!
	// ========== Fin Asignacion Variable: n ===========
	// ======== Fin Bloque =========
	b  L0
L1:
	// ========== Fin While ===========
	// =========== Print ===========
	// --------- Primitivo ---------
	// --------- Cadena ---------
	str x10, [sp, #-8]!
	mov w0, #10
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #32
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #99
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #105
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #99
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #108
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #111
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #32
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #102
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #111
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #114
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #32
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #99
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
	mov w0, #115
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #105
	strb w0, [x10]
	mov x0, #1
	add x10, x10, x0
	mov w0, #99
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
	// ========== Instruccion For ===========
	// ========== Declaración: i ===========
	// --------- Primitivo ---------
	// --------- Entero ---------
	mov x0, #0
	str x0, [sp, #-8]!
	// ------- Fin Entero -------
	// ------- Fin Primitivo -------
	// ========== Fin Declaración ===========
L7:
	// ----- Relacional (<=) -----
	// ========== Acceso a Variable: i ===========
	mov x0, #0
	add x0, sp, x0
	ldr x0, [x0, #0]
	str x0, [sp, #-8]!
	// ========== Fin Acceso Variable: ===========
	// --------- Primitivo ---------
	// --------- Entero ---------
	mov x0, #5
	str x0, [sp, #-8]!
	// ------- Fin Entero -------
	// ------- Fin Primitivo -------
	ldr x0, [sp], #8
	ldr x1, [sp], #8
	cmp x1, x0 
	ble  L10
	mov x0, #0
	str x0, [sp, #-8]!
	b  L11
L10:
	mov x0, #1
	str x0, [sp, #-8]!
L11:
	str x0, [sp, #-8]!
	// --- Fin Relacionales (<=) ---
	ldr x0, [sp], #8
	cbz x0, L8 
	// ========== Bloque ===========
	// ========== Instruccion If ===========
	// ----- Relacional (==) -----
	// ========== Acceso a Variable: i ===========
	mov x0, #8
	add x0, sp, x0
	ldr x0, [x0, #0]
	str x0, [sp, #-8]!
	// ========== Fin Acceso Variable: ===========
	// --------- Primitivo ---------
	// --------- Entero ---------
	mov x0, #3
	str x0, [sp, #-8]!
	// ------- Fin Entero -------
	// ------- Fin Primitivo -------
	ldr x0, [sp], #8
	ldr x1, [sp], #8
	cmp x1, x0 
	beq  L12
	mov x0, #0
	str x0, [sp, #-8]!
	b  L13
L12:
	mov x0, #1
	str x0, [sp, #-8]!
L13:
	str x0, [sp, #-8]!
	// --- Fin Relacionales (==) ---
	ldr x0, [sp], #8
	cbz x0, L14 
	// ========== Bloque ===========
	// Sentencia Continue
	b  L9
	// ======== Fin Bloque =========
L14:
	// ========== Fin If ===========
	// =========== Print ===========
	// ========== Acceso a Variable: i ===========
	mov x0, #16
	add x0, sp, x0
	ldr x0, [x0, #0]
	str x0, [sp, #-8]!
	// ========== Fin Acceso Variable: ===========
	ldr x0, [sp], #8
	mov x0, x0
	bl print_integer
	bl print_new_line
	// ========= Fin Print =========
	// ---------- Removiendo Bytes de la Pila ----------
	mov x0, #8
	add sp, sp, x0
	// ------------ Stack Pointer Ajustado -------------
	// ======== Fin Bloque =========
L9:
	// ========== Asignacion Variable: i ===========
	// ----- Aritmetica (+) -----
	// ========== Acceso a Variable: i ===========
	mov x0, #8
	add x0, sp, x0
	ldr x0, [x0, #0]
	str x0, [sp, #-8]!
	// ========== Fin Acceso Variable: ===========
	// --------- Primitivo ---------
	// --------- Entero ---------
	mov x0, #1
	str x0, [sp, #-8]!
	// ------- Fin Entero -------
	// ------- Fin Primitivo -------
	ldr x0, [sp], #8
	ldr x1, [sp], #8
	add x0, x0, x1
	str x0, [sp, #-8]!
	// --- Fin Aritmetica (+) ---
	ldr x0, [sp], #8
	mov x1, #8
	add x1, sp, x1
	str x0, [x1, #0]
	str x0, [sp, #-8]!
	// ========== Fin Asignacion Variable: i ===========
	b  L7
L8:
	// ---------- Removiendo Bytes de la Pila ----------
	mov x0, #24
	add sp, sp, x0
	// ------------ Stack Pointer Ajustado -------------
	// ========== Fin For ===========
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
// print_integer - Prints a signed integer to stdout
//
// Input:
//   x0 - The integer value to print
//--------------------------------------------------------------
print_integer:
    // Save registers
    stp x29, x30, [sp, #-16]!  // Save frame pointer and link register
    stp x19, x20, [sp, #-16]!  // Save callee-saved registers
    stp x21, x22, [sp, #-16]!
    stp x23, x24, [sp, #-16]!
    stp x25, x26, [sp, #-16]!
    stp x27, x28, [sp, #-16]!

    // Check if number is negative
    mov x19, x0                // Save original number
    cmp x19, #0                // Compare with zero
    bge positive_number        // Branch if greater or equal to zero

    // Handle negative number
    mov x0, #1                 // fd = 1 (stdout)
    adr x1, minus_sign         // Address of minus sign
    mov x2, #1                 // Length = 1
    mov w8, #64                // Syscall write
    svc #0

    neg x19, x19               // Make number positive

positive_number:
    // Prepare buffer for converting result to ASCII
    sub sp, sp, #32            // Reserve space on stack
    mov x22, sp                // x22 points to buffer

    // Initialize digit counter
    mov x23, #0                // Digit counter

    // Handle special case for zero
    cmp x19, #0
    bne convert_loop

    // If number is zero, just write '0'
    mov w24, #48               // ASCII '0'
    strb w24, [x22, x23]       // Store in buffer
    add x23, x23, #1           // Increment counter
    b print_result             // Skip conversion loop

convert_loop:
    // Divide the number by 10
    mov x24, #10
    udiv x25, x19, x24         // x25 = x19 / 10 (quotient)
    msub x26, x25, x24, x19    // x26 = x19 - (x25 * 10) (remainder)

    // Convert remainder to ASCII and store in buffer
    add x26, x26, #48          // Convert to ASCII ('0' = 48)
    strb w26, [x22, x23]       // Store digit in buffer
    add x23, x23, #1           // Increment digit counter

    // Prepare for next iteration
    mov x19, x25               // Quotient becomes the new number
    cbnz x19, convert_loop     // If number is not zero, continue

    // Reverse the buffer since digits are in reverse order
    mov x27, #0                // Start index
reverse_loop:
    sub x28, x23, x27          // x28 = length - current index
    sub x28, x28, #1           // x28 = length - current index - 1

    cmp x27, x28               // Compare indices
    bge print_result           // If crossed, finish reversing

    // Swap characters
    ldrb w24, [x22, x27]       // Load character from start
    ldrb w25, [x22, x28]       // Load character from end
    strb w25, [x22, x27]       // Store end character at start
    strb w24, [x22, x28]       // Store start character at end

    add x27, x27, #1           // Increment start index
    b reverse_loop             // Continue reversing

print_result:
    // Print the result
    mov x0, #1                 // fd = 1 (stdout)
    mov x1, x22                // Buffer address
    mov x2, x23                // Buffer length
    mov w8, #64                // Syscall write
    svc #0

    // Clean up and restore registers
    add sp, sp, #32            // Free buffer space
    ldp x27, x28, [sp], #16    // Restore callee-saved registers
    ldp x25, x26, [sp], #16
    ldp x23, x24, [sp], #16
    ldp x21, x22, [sp], #16
    ldp x19, x20, [sp], #16
    ldp x29, x30, [sp], #16    // Restore frame pointer and link register
    ret                        // Return to caller

new_line: .ascii "\n"
minus_sign: .ascii "-"