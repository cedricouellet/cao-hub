# Receipt Management

[DrawSQL Database Diagram](https://drawsql.app/teams/cedricao/diagrams/caohub-receipt-management)

## Domain

Before diving into the business concepts, here is some explanation of the terminology used:

| Term          | Meaning                                                                                                                             |
| ------------- | ----------------------------------------------------------------------------------------------------------------------------------- |
| Has           | indicates a required property of a business entity                                                                                  |
| Can have      | indicates an optional property of a business entity                                                                                 |
| References    | indicates a relationship that is required, but not tightly coupled (finding one without the other would maintain logical sense)     |
| Can reference | indicates a relationship that is optional                                                                                           |
| Belongs to    | indicates a relationship that is required and tightly couples (you could not find one without the other and maintain logical sense) |

### Store Category

- Has a unique name
- Can be disabled, hiding it from results
- Is referenced by many stores

### Store

- References a store category
- Has a unique name
- Can be disabled, hiding it from results
- Can be used by N receipts

### Receipt

- References a store
- Has a specific date
- Is paid by a single person
- Can be disabled, hiding it from results
- Can be used by N receipt items

### Receipt Item

- Belongs to a receipt
- References a product
- Has a quantity
- Has a unit price, pre-tax (should later be multiplied by the quantity)
- Can have a unit discount, pre-tax (should later be multiplied by the quantity)
- Can have multiple taxes applied to it
- Can be paid by N people
- Can be disabled, hiding it from results

### Tax

- Has a name
- Can have a description
- Has a rate (e.g, 0.975)
- Can be disabled, hiding it from results
- Can be referenced by N receipt items

### Person

- Has a name
- Can be referenced by multiple receipts
- Can be referenced by multiple receipt items
- Can be disabled, hiding it from results

[Back](/README.md)
