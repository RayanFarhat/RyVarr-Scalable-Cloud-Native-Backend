<script>
  import { page } from '$app/stores';  
</script>

<ul class="menu p-4 w-60 min-h-full bg-base-200 flex items-center">
    <!-- Sidebar content here -->
    <li><a class="p-0 w-fit" href="/pricing">
      <button class="btn btn-outline border-0 hover:bg-transparent hover:text-primary">
        Pricing</button></a></li>
        
    <li><a class="mb-8 p-0 w-fit" href="/product/hamzacad">
      <button class="btn btn-outline border-0 hover:bg-transparent hover:text-primary">
        Hamzacad</button></a></li>

    <li><a class="p-0 w-fit" href="/login">
      <button class="btn btn-outline btn-primary">
        Login</button></a></li>

    <li><a class="p-0 w-fit" href="/2">
      <button class="btn btn-primary">
        {
          $page.url.pathname.includes("/product/hamzacad")? "Download":"Let's Talk"
          }</button></a></li>
  </ul>