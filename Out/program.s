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
	// ========== Declaración: n ===========
	// --------- Primitivo ---------
	// -------- Flotante --------
	movz x0, #-5977, lsl #0
	movk x0, #11848, lsl #16
	movk x0, #8703, lsl #32
	movk x0, #16393, lsl #48
	str x0, [sp, #-8]!
	// ------ Fin Flotante ------
	// ------- Fin Primitivo -------
	// ========== Fin Declaración ===========
	// =========== Print ===========
	// ========== Acceso a Variable: saludo ===========
	mov x0, #8
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
	bl print_space
	// ========== Acceso a Variable: n ===========
	mov x0, #0
	add x0, sp, x0
	ldr x0, [x0, #0]
	str x0, [sp, #-8]!
	// ========== Fin Acceso Variable: ===========
	ldr x0, [sp], #8
	mov x0, x0
	bl print_double
	bl print_new_line
	// ========= Fin Print =========
	// ---------- Removiendo Bytes de la Pila ----------
	mov x0, #16
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
// print_double - Prints a double precision float to stdout
//
// Input:
//   d0 - The double value to print
//--------------------------------------------------------------
print_double:
    // Save context
    stp x29, x30, [sp, #-16]!    
    stp x19, x20, [sp, #-16]!
    stp x21, x22, [sp, #-16]!
    stp x23, x24, [sp, #-16]!
    
    // Check if number is negative
    fmov x19, d0
    tst x19, #(1 << 63)       // Comprueba el bit de signo
    beq skip_minus

    // Print minus sign
    mov x0, #1
    adr x1, minus_sign
    mov x2, #1
    mov x8, #64
    svc #0

    // Make value positive
    fneg d0, d0

skip_minus:
    // Convert integer part
    fcvtzs x0, d0             // x0 = int(d0)
    bl print_integer

    // Print dot '.'
    mov x0, #1
    adr x1, dot_char
    mov x2, #1
    mov x8, #64
    svc #0

    // Get fractional part: frac = d0 - float(int(d0))
    frintm d4, d0             // d4 = floor(d0)
    fsub d2, d0, d4           // d2 = d0 - floor(d0) (exact fraction)

    // Para 2.5, d2 debe ser exactamente 0.5

    // Multiplicar por 1_000_000 (6 decimales)
    movz x1, #0x000F, lsl #16
    movk x1, #0x4240, lsl #0   // x1 = 1000000
    scvtf d3, x1              // d3 = 1000000.0
    fmul d2, d2, d3           // d2 = frac * 1_000_000
    
    // Redondear al entero más cercano para evitar errores de precisión
    frintn d2, d2             // d2 = round(d2)
    fcvtzs x0, d2             // x0 = int(d2)

    // Imprimir ceros a la izquierda si es necesario
    mov x20, x0               // x20 = fracción entera
    movz x21, #0x0001, lsl #16
    movk x21, #0x86A0, lsl #0  // x21 = 100000
    mov x22, #0               // inicializar contador de ceros
    mov x23, #10              // constante para división

leading_zero_loop:
    udiv x24, x20, x21        // x24 = x20 / x21
    cbnz x24, done_leading_zeros  // Si hay un dígito no cero, salir del bucle

    // Imprimir '0'
    mov x0, #1
    adr x1, zero_char
    mov x2, #1
    mov x8, #64
    svc #0

    udiv x21, x21, x23        // x21 /= 10
    add x22, x22, #1          // incrementar contador de ceros
    cmp x21, #0               // verificar si llegamos al final
    beq print_remaining       // si divisor es 0, saltar a imprimir el resto
    b leading_zero_loop

done_leading_zeros:
    // Print the remaining fractional part
    mov x0, x20
    bl print_integer
    b exit_function

print_remaining:
    // Caso especial cuando la parte fraccionaria es 0 después de imprimir ceros
    cmp x20, #0
    bne exit_function

    // Ya imprimimos todos los ceros necesarios
    // No hace falta imprimir nada más

exit_function:
    // Restore context
    ldp x23, x24, [sp], #16
    ldp x21, x22, [sp], #16
    ldp x19, x20, [sp], #16
    ldp x29, x30, [sp], #16
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
dot_char: .ascii "."
zero_char: .ascii "0"