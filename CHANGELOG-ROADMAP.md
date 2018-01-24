# Portal CMS Roadmap / Changelog
This file contains a list of changes between versions of Portal CMS and a roadmap that provides a brief outline of the features we intend to include in Portal CMS.

## Future Intentions
1 Change Authentication Method to use Owin (Unplanned)
1. Improved Menu Management with more Visual Options OOTB (Unplanned)

# Changelog

## 1.3.4 - (January 2018) Maintenance Release
1. Upgrade SendGrid Implementation (Requires Configuration for Existing Websites)
2. Improved User Experience via Modal Popvers to Reduce Post Backs and Improve Ease of Use
3. Improving Code Quality using Codacy Automated Code Reviews
4. Improved Organisation of Website Assets and Plugins
5. Content Delivery Network now covers a wider range of website content
6. Extraction of Authentication Actions to a New Area to Support Integration of Owin Pipeline in a later release.
7. New Components and Section Types available
8. New Widget for displaying Blog Posts

## 1.3.3 - (December 2017) Maintenance Release: General Update with Minor Changes
1. Change the Default Connection String to not use an SQLExpress Endpoint.
2. Consolidation of the 'Edit Freestyle' action as logic was duplicated
3. Mobile Responsiveness Improvements to the Blog Pages
4. Improve Usability in the Page Builder by making containers larger when you are trying to drag a component into them.
5. Update Packages and Compilers to latest stable verisons
6. Resolve a bug where widget previews would not load immediately in the 'Add Component' popup
7. Improve the Visual Appearance of the Page Builder section controls to make it possible to add a much more capable editing experience in future.
8. Make the Page Builder Quick Controls smaller.

## 1.3.2 - (October 2017) Maintenance Release: I am using Portal CMS for another personal project, so I am bringing useful changes back into the PortalCMS repository when applicable.
1. Improve Performance while Interacting with the Component Controller in the PageBuilder.
2. New Ability to Show Content to Anonymous Users Only.

## 1.3.1 - (August 2017) Continued Focus: Performance Optimisation
1. Service layer refactored to be Asynchronous so that Calls to Entity Framework and other external services cannot cause a negative User Experience.
2. Cache Retrieval of Menu Systems so that they do not need to hit the database on every page load.
3. Reduce use of Html.Action to load associated functionality, now loaded through Ajax in all instances.
4. Increased Output Caching on Rarely Changing Actions.
5. Read Only Session Use to Reduce Session Locking Risks.

## 1.3 - (August 2017) Focus: Performance Optimisation, Improvements to the Page Builder and making it easier to develop bespoke functionality
1. Ability to clone components in the page builder, helping you build up pages even quicker.
2. Ability to display the same section on multiple pages at once. Meaning if you update it in one place, it updates everywhere.
3. Ability to write your own MVC Partial Actions and render them on Page Builder pages.
3. Ability to configure a CDN Endpoint and have Portal CMS load some assets (Images, Scripts and Styles) from a CDN. Tested with Azure CDN.
4. Changes to the Widget Functionality to make it easier to write bespoke widgets, and Introduce several new OOTB widgets which can be dragged onto pages in the Page Builder.
5. Structural Updates to the Source code to organise Code in Areas, Make Other Files like Styles and Scripts easier to maintain and find.
6. New and Updated Sample Images and Icons, optimised for performance
7. New Bootstrap Alert Components amongst other new component types available for use in the Page Builder
8. Modify Introductory process and seeded landing pages.
10. Add Hover.CSS and Animate.CSS libraries for use in the Page Builder and give you the ability to apply animations on Containers.
11. Add a new Carousel Component and new Section Types Involving it.
12. Updated Back End Technologies to use the latest versions available such as C# 7.
13. Page Load Speed Optimisations through smarter use of bundles and only loading required assets when required (Then caching them and using a CDN when configured).
14. Add a default Favicon to the website.
15. Renewed support for the Editor user Role allowing non administrator users to add new content.
16. Improved user experience across the page builder on Tablet and Desktop.
17. Improved Administration user experience when you are logged out while editing a modal window, you are automatically redirected to login again.

## 1.2 - (March 2017) Focus: Increase Feature Set and Improve User Experience.
1. User Experience: Brand New Quick Access Panel for Administrator Users.
2. User Experience: Page Builder no longer posts back when: Adding a Section, Editing Section Markup, Editing an Image, Editing a Section, Adding a Component, Deleting a Section, Deleting a Component.
3. Architecture: Ability to Backup and Restore individual sections on demand.
4. User Experience: Animations added across the Website to provide feedback when events occur.
5. User Experience: Paging Implemented on parts of the site that have growable content.
6. Architecture: Add LogBook to provide trapping and display of Application level errors.
7. Architecture: Better User Experience when encountering Server Errors. New Error Pages available which can be customised in the Page Builder.
8. Administration Panel: New Analytic Graph showing Requests vs Errors and a detail page listing actual errors with Exception details.
9. User Experience: Improvements to the Blog Pages that allow an Administrator to change Headline and Descriptions for an article in line on the blog post.
10. Architecture: Significant Performance Optimisations to Client Side Scripting and Resource Management.
11. A Wide Variety of new Section Types available for use in the Page Builder.
12. Ability to add Embedded Videos into Custom Pages.
13. Ability to add Image Carousels into Custom Pages.
14. Improvements made to the Flexible Image Component to make it more flexible!
15. Added Blog Manager App Drawer for Quick Access to Blog Posts.
16. User Experience: Ability to "Quick Change" between Add Section and Add Component Screens.
17. User Experience: Ability to Move App Drawers (Useful for Mobile Devices so content is not hidden).
18. Architecture: Removed Bootstrap Tour Elements from the Page Builder.
19. Architecture: SEO Improvements to aid Page Ranking.
20. User Experience: Improvements to the User Experience when editing Pages on Mobile or Tablet.
21. Ground work completed ahead of next release, to make adding new component and section types much quicker and easier.
22. Architecture: Refactoring to comply with Clean Code Rules
23. Multiple usability and performance improvements across the framework.

## 1.1 - (November 2016) Focus: Increasing Feature Set with oft expected features.
1. Improvements to website reliability and architecture.
2. Improved performance following Performance testing.
3. Ability to upload your own Font's and use them in custom Themes.
4. Ability to create custom themes including fonts, text sizes and colour schemes to change your website's appearance.
5. Ability to apply solid colour backgrounds to Sections in addition to picture backgrounds.
6. Improved Menu Management Tools
7. Additional App Drawers exposed on the Page Builder: Page Manager and Theme Manager drawers.
8. User Interface improvements following user testing.
9. A variety of new component and section types.
10. Create a contact form Widget.
11. A tour shown to new administrators to help understand the functions of the Page Builder.
11. Incorporation of a Unit Testing Technology.
12. Change website from use of Glyphicons to Font Awesome.

## 1.0 - Initial Release
1. Ability to use the Administration Panel with the following features...
  1. Dashboard
  2. Media Manager (Upload Images)
  3. Post Manager (Create and Edit Blog Posts
  4. Page Manager (Manage your Custom Pages)
  5. Copy Manager (Ability to use and edit custom Copy)
  6. Analytics Manager (Internal Analytics Engine with Reporting)
  7. User Manager (Ability to review, edit and create users)
  8. Settings Manager (Ability to edit basic settings, roles and menu's
2. Ability to use the Page Builder
  1. Ability to add Template Sections
  2. Ability to add Components
  3. Ability to edit components with inline editing tools
  4. Ability to change order of sections within your page
3. Ability to register and sign in
  1. Including the ability to manage your profile such as Avatar, Bio, Details and Password.
4. Abilities to manage and view blog posts
  1. Including Inline editing for changing your posts quickly
  2. Including Comments, Photo Galleries and Tie ins to other posts
