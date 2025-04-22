<template>
  <div class="search-container">
    <h3>Search Submissions</h3>
    <input type="text" v-model="searchQuery" placeholder="Search by name or category..." class="search-box" />
    <div class="results">
      <div v-if="filteredSubmissions.length > 0">
        <div v-for="submission in filteredSubmissions" :key="submission.id" class="result-item">
          <strong>{{ submission.data.name }}</strong> — {{ submission.data.category }} 
          ({{ formatDate(submission.data.date) }}) | Priority: {{ submission.data.priority || 'N/A' }}
          <div class="submission-date">Submitted: {{ formatSubmissionDate(submission.submittedAt) }}</div>
        </div>
      </div>
      <div v-else class="no-results">
        No submissions found.
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'SubmissionList',
  props: ['submissions'],
  data() {
    return {
      searchQuery: ''
    };
  },
  computed: {
    filteredSubmissions() {
      if (!this.submissions || this.submissions.length === 0) {
        return [];
      }
      
      if (!this.searchQuery) {
        return this.submissions;
      }
      
      const query = this.searchQuery.toLowerCase();
      return this.submissions.filter(sub => {
        // Make sure data exists and has name and category properties
        if (!sub.data) return false;
        
        const nameMatch = sub.data.name && 
          sub.data.name.toLowerCase().includes(query);
        const categoryMatch = sub.data.category && 
          sub.data.category.toLowerCase().includes(query);
        
        return nameMatch || categoryMatch;
      });
    }
  },
  methods: {
    formatDate(dateString) {
      if (!dateString) return 'N/A';
      return new Date(dateString).toLocaleDateString();
    },
    formatSubmissionDate(dateString) {
      if (!dateString) return 'N/A';
      return new Date(dateString).toLocaleString();
    }
  }
};
</script>

<style>
.search-container {
  background: #fff;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  margin-bottom: 20px;
  width: 80%;
  margin-left: auto;
  margin-right: auto;
}
.search-box {
  border: 2px solid #007bff;
  border-radius: 4px;
  padding: 8px;
  width: 100%;
  margin-bottom: 10px;
}
.results {
  margin-top: 10px;
}
.result-item {
  background: #f8f9fa;
  padding: 12px;
  border-radius: 4px;
  margin-bottom: 8px;
  border: 1px solid #ddd;
}
.no-results {
  color: #777;
  font-style: italic;
  margin-top: 10px;
}
.submission-date {
  color: #666;
  font-size: 0.8rem;
  margin-top: 4px;
}
</style>