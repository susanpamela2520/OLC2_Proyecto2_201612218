namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;

public enum R {
    // Registros de Propósito General
    x0,  x1,  x2,  x3,  x4,  x5,  x6,  x7,
    x8,  x9,  x10, x11, x12, x13, x14, x15,
    x16, x17, x18, x19, x20, x21, x22, x23,
    x24, x25, x26, x27, x28, x29, x30,

    // Registros de Punto Flotante
    v0,  v1,  v2,  v3,  v4,  v5,  v6,  v7,
    v8,  v9,  v10, v11, v12, v13, v14, v15,
    v16, v17, v18, v19, v20, v21, v22, v23,
    v24, v25, v26, v27, v28, v29, v30, v31,

    // Registros Especiales
    sp,       // Stack Pointer
    pc,       // Program Counter
    xzr,      // Zero Register

    // Alias
    hp = x10, // "Heap Pointer"
    fp = x29, // Frame Pointer
    lr = x30, // Link Register
}