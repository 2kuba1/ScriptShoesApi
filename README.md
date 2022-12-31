# ScriptShoesApi

## General info

Monolith + CQRS api which has basic e-commerce shop features.

The project was created to practice my skills in ASP.NET Core.

## Technologies

 * ASP.NET Core 7
 * MediatR
 * Entity Framework Core 7
 * JWT.NET
 * BCrypt
 * AutoMapper
 * FluentValidation
 * MailKit
 * Microsoft SQL Server
 
 ## Database
 
![database](https://user-images.githubusercontent.com/71401593/203863876-f59daad3-7268-4935-a144-4987a4eca96b.png)

## Main features:

### Cart
 *  Add items to cart
 *  Delete item from cart
 *  Get items from cart
 
### Admin Panel
 * Add shoe to store
 * Add shoe image
 * Add shoe main image
 * Delte shoe
 * Update shoe
 * Update image
 * Update main image
 * Update shoe
 
### Reviews:
 *  Add like to review
 *  Remove like from review
 *  Create review
 *  Delete review
 *  Update review
 *  Get available reviews
 *  Get user liked reviews
 *  Get review statistics
 *  Get shoe reviews
      
### Shoes:
 * Get all shoes
 * Get filters
 * Get shoes by name
 * Get shoe with content
   
### Favorites:
  * Add to favorites 
  * Remove shoe from favorites
  * Get favorites
   
### Users:
  * Add profile picture
  * Register/Create user
  * Delete profile picture
  * Send email with activation code
  * Send email with new activation code
  * Verify email
  * Login
  * Refresh tokens
    
 ### Logs:
   * Discord webhooks which are sending server errors/exceptions/informations to the server
