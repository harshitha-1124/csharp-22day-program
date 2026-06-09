<script setup>
import { ref, onMounted, computed } from 'vue'

const departments = ref([])
const grandTotal = ref(0)


const sortedDepartments = computed(() => {
  return [...departments.value].sort((a, b) => b.total - a.total)
})


const maxTotal = computed(() => {
  return Math.max(...departments.value.map(d => d.total || 0))
})

onMounted(async () => {
  const response = await fetch('https://localhost:7241/api/getData')
  const result = await response.json()

  departments.value = result.data
  grandTotal.value = result.grandTotal
})
</script>

<template>
  <table >
    
    
    <tr class="header-row">
      <th>department</th>
      <th>inpatient</th>
      <th>outpatient</th>
      <th>ed</th>
      <th>total</th>
    </tr>

    
    <tr
      v-for="d in sortedDepartments"
      :key="d.departmentName"
      :class="{ 'highlight-row': d.total === maxTotal }"
    >
      <td>{{ d.departmentName }}</td>
      <td>{{ d.inpatient }}</td>
      <td>{{ d.outpatient }}</td>
      <td>{{ d.ed }}</td>
      <td>{{ d.total }}</td>
    </tr>

   
    <tr>
      <td colspan="4"><b>grandttl</b></td>
      <td><b>{{ grandTotal }}</b></td>
    </tr>

  </table>
</template>

<style>
.header-row {
  background-color: #ddd;
  
}

.highlight-row {
  background-color: lightpink;
  
}
</style>